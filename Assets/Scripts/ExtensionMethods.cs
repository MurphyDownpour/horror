using UnityEngine;
using UnityVector3 = UnityEngine.Vector3;
using BaseVector3 = System.Numerics.Vector3;

namespace Core.Utility
{
    public static class ExtensionMethods
    {
        public static void Switch(this GameObject obj)
        {
            var state = obj.activeInHierarchy;
            obj.SetActive(!state);
        }

        public static BaseVector3 ToBase(this UnityVector3 unityVector)
        {
            return new BaseVector3(unityVector.x, unityVector.y, unityVector.z);
        }
    
        public static UnityVector3 ToUnity(this BaseVector3 unityVector)
        {
            return new UnityVector3(unityVector.X, unityVector.Y, unityVector.Z);
        }
    }

}