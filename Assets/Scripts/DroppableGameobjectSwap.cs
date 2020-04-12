using UnityEngine;

namespace Humio
{
    public class DroppableGameobjectSwap : Droppable
    {
        [SerializeField] private GameObject oldGameObject;
        [SerializeField] private GameObject newGameObject;

        public override void Trigger()
        {
            base.Trigger();
            oldGameObject.SetActive(false);
            newGameObject.SetActive(true);
        }
    }
}