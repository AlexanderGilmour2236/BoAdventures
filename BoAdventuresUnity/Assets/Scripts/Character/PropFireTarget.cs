using UnityEngine;

namespace BoAdventures
{
   public class PropFireTarget : MonoCharacterView, ITargetObject
   {
      public void TakeDamage(int damage)
      {
         
      }

      public Transform Transform
      {
         get { return transform; }
      }
      
      public FireTargetType FireTargetType
      {
         get { return FireTargetType.Enemy; }
      }
   } 
}
