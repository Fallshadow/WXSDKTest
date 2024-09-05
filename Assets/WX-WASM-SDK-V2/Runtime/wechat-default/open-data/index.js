/* eslint-disable indent */
import { getFriendRankData, getGroupFriendsRankData, setUserRecord } from './data/index';
import getFriendRankXML from './render/tpls/friendRank';
import getFriendRankStyle from './render/styles/friendRank';
import getTipsXML from './render/tpls/tips';
import getTipsStyle from './render/styles/tips';
import { showLoading } from './loading';
const Layout = requirePlugin('Layout').default;
const RANK_KEY = 'user_rank';
const sharedCanvas = wx.getSharedCanvas();
const sharedContext = sharedCanvas.getContext('2d');

const MessageType = {
    WX_RENDER: 'WXRender',
    WX_DESTROY: 'WXDestroy',
    SHOW_FRIENDS_RANK: 'showFriendsRank',
    SET_USER_RECORD: 'setUserRecord',
};

/**
 * 初始化开放域，主要是使得 Layout 能够正确处理跨引擎的事件处理
 * 如果游戏里面有移动开放数据域对应的 RawImage，也需要抛事件过来执行Layout.updateViewPort
 */
const initOpenDataCanvas = async (data) => {
    Layout.updateViewPort({
        x: data.x / data.devicePixelRatio,
        y: data.y / data.devicePixelRatio,
        width: data.width / data.devicePixelRatio,
        height: data.height / data.devicePixelRatio,
    });
};

// 给定 xml 和 style，渲染至 sharedCanvas
function LayoutWithTplAndStyle(xml, style) {
    Layout.clear();
    Layout.init(xml, style);
    Layout.layout(sharedContext);
    console.log(Layout);
}

// 仅仅渲染一些提示，比如数据加载中、当前无授权等
function renderTips(tips = '') {
    LayoutWithTplAndStyle(getTipsXML({
        tips,
    }), getTipsStyle({
        width: sharedCanvas.width,
        height: sharedCanvas.height,
    }));
}

// 将好友排行榜数据渲染在 sharedCanvas
async function renderFriendsRank() {
    showLoading();
    try {
        const data = await getFriendRankData(RANK_KEY);
        if (!data.length) {
            renderTips('暂无好友数据');
            return;
        }
        LayoutWithTplAndStyle(getFriendRankXML({
            data,
        }), getFriendRankStyle({
            width: sharedCanvas.width,
            height: sharedCanvas.height,
        }));
    }
    catch (e) {
        console.error('renderFriendsRank error', e);
        renderTips('请进入设置页允许获取微信朋友信息');
    }
}

function main() {
    wx.onMessage((data) => {
        console.log('[WX OpenData] onMessage', data);
        if (typeof data === 'string') {
            try {
                // eslint-disable-next-line no-param-reassign
                data = JSON.parse(data);
            }
            catch (e) {
                console.error('[WX OpenData] onMessage data is not a object');
                return;
            }
        }
        switch (data.type) {
            // 来自 WX Unity SDK 的信息
            case MessageType.WX_RENDER:
                initOpenDataCanvas(data);
                break;
            // 来自 WX Unity SDK 的信息
            case MessageType.WX_DESTROY:
                Layout.clearAll();
                break;
            // 下面为业务自定义消息
            case MessageType.SHOW_FRIENDS_RANK:
                renderFriendsRank();
                break;
            case MessageType.SET_USER_RECORD:
                setUserRecord(RANK_KEY, data.score);
                break;
            default:
                console.error(`[WX OpenData] onMessage type 「${data.type}」 is not supported`);
                break;
        }
    });
}
main();
