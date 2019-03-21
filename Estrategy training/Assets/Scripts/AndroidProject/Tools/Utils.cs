using UnityEngine;

namespace Assets.Scripts.AndroidProject.Tools
{
   public static class Utils
    {
        public static Vector3 ConvertToVector3(this Vector2 newPosition, Transform characher)
        {
            Vector2 positionFinger = Camera.main.WorldToScreenPoint(newPosition);
            Vector3 finger = new Vector3();
            Ray ray = Camera.main.ScreenPointToRay(newPosition);
            // create a logical plane at this object's position
            // and perpendicular to world Y:
            Plane plane = new Plane(Vector3.up, characher.position);
            float distance = 0; // this will return the distance from the camera
            if (plane.Raycast(ray, out distance))
            { // if plane hit...
                finger = ray.GetPoint(distance); // get the point
            }                               // pos has the position in the plane you've touche
            return finger;
        }
    }
}
