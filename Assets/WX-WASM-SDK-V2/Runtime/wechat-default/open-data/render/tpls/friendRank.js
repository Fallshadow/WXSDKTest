/**
 * 下面的内容分成两部分，第一部分是一个模板，模板的好处是能够有一定的语法
 * 坏处是模板引擎一般都依赖 new Function 或者 eval 能力，小游戏下面是没有的
 * 所以模板的编译需要在外部完成，可以将注释内的模板贴到下面的页面内，点击 "run"就能够得到编译后的模板函数
 * https://wechat-miniprogram.github.io/minigame-canvas-engine/playground.html
 * 如果觉得模板引擎使用过于麻烦，也可以手动拼接字符串，本文件对应函数的目标仅仅是为了创建出 xml 节点数
 */
/*
<view class="container" id="main">
  <view class="rankList">
    <scrollview class="list" scrollY="true">
      {{~it.data :item:index}}
        <view class="listItem">
          <image src="open-data/render/image/rankCommonBg.png" class="rankCommonBg"></image>
          <text class="rankIndex" value="{{=index || 0}}"></text>
          <image class="rankAvatar" src="{{= item.avatarUrl }}"></image>
          <text class="rankName" value="{{=item.nickname}}"></text>
          <text class="rankScoreVal" value="{{=item.score || 0}}"></text>
          <text class="rankScoreTip" value="分"></text>
        </view>
      {{~}}
    </scrollview>
  </view>
</view>
*/
/**
 * xml经过doT.js编译出的模板函数
 * 因为小游戏不支持new Function，模板函数只能外部编译
 * 可直接拷贝本函数到小游戏中使用
 */
export default function anonymous(it) {
  let out = '<view class="container" id="main"> <view class="rankList"> <scrollview class="list" scrollY="true"> ';
  const arr1 = it.data;
  if (arr1) {
      let item;
      let index = -1;
      const l1 = arr1.length - 1;
      while (index < l1) {
          item = arr1[(index += 1)];

          if(index == 0){
            out += ` <view class="listItem"> <image src="open-data/render/image/rankOneBg.png" class="rankCommonBg"></image> <text class="rankIndex" value="${(index + 1) || 0}"></text> <image class="rankAvatar" src="${item.avatarUrl}"></image> <text class="rankName" value="${item.nickname}"></text> <text class="rankScoreVal" value="${item.score || 0}"></text> <text class="rankScoreTip" value="分"></text> </view> `;
          }
          else if(index == 1){
            out += ` <view class="listItem"> <image src="open-data/render/image/rankTwoBg.png" class="rankCommonBg"></image> <text class="rankIndex" value="${(index + 1) || 0}"></text> <image class="rankAvatar" src="${item.avatarUrl}"></image> <text class="rankName" value="${item.nickname}"></text> <text class="rankScoreVal" value="${item.score || 0}"></text> <text class="rankScoreTip" value="分"></text> </view> `;
          }
          else if(index == 2){
            out += ` <view class="listItem"> <image src="open-data/render/image/rankThreeBg.png" class="rankCommonBg"></image> <text class="rankIndex" value="${(index + 1) || 0}"></text> <image class="rankAvatar" src="${item.avatarUrl}"></image> <text class="rankName" value="${item.nickname}"></text> <text class="rankScoreVal" value="${item.score || 0}"></text> <text class="rankScoreTip" value="分"></text> </view> `;
          }
          else{
            out += ` <view class="listItem"> <image src="open-data/render/image/rankCommonBg.png" class="rankCommonBg"></image> <text class="rankIndex" value="${(index + 1) || 0}"></text> <image class="rankAvatar" src="${item.avatarUrl}"></image> <text class="rankName" value="${item.nickname}"></text> <text class="rankScoreVal" value="${item.score || 0}"></text> <text class="rankScoreTip" value="分"></text> </view> `;
          }


      }
  }
  out += ' </scrollview> </view></view>';
  return out;
}
