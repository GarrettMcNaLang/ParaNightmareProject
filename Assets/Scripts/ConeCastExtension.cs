
using System.Collections.Generic;
using UnityEngine;

public static class ConeCastExtension
{
    //Angelic Choir: https://github.com/walterellisfun/ConeCast
   
        public static RaycastHit[] ConeCastAll(this Physics physics, Vector3 origin, Vector3 direction, float MaxRadius, float MaxDistance, float ConeAngle)
        {
            RaycastHit[] sphereCastHits = Physics.SphereCastAll(origin - new Vector3(0, 0, MaxRadius), MaxRadius, direction, MaxDistance);
            List<RaycastHit> coneCastHitList = new List<RaycastHit>();

            if(sphereCastHits.Length > 0)
             {
                for(int i = 0; i < sphereCastHits.Length; i++)
                {
                    sphereCastHits[i].collider.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
                    Vector3 hitPoint = sphereCastHits[i].point;
                    Vector3 directionToHit = hitPoint - origin;
                    float angleToHit = Vector3.Angle(direction, directionToHit);

                    if(angleToHit < ConeAngle)
                    {
                    coneCastHitList.Add(sphereCastHits[i]);
                    }

                }
             }

         RaycastHit[] coneCastHits = new RaycastHit[coneCastHitList.Count];
         coneCastHits = coneCastHitList.ToArray();

            return coneCastHits;

        }


    
}
