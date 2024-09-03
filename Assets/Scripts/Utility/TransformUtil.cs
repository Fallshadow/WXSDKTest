using UnityEngine;

public static class TransformUtil {
    public static Transform DeepFind(this Transform aParent, string aName) {
        if(aParent.name == aName) return aParent;
        foreach(Transform child in aParent) {
            if(child.name == aName) {
                return child;
            }
            var result = DeepFind(child, aName);
            if(result != null) {
                return result;
            }
        }
        return null;
    }
}
