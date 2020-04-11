using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Humio
{
    public class DroppableProducer : Droppable
    {
        [SerializeField] private List<Item> produces;

        public override void Trigger()
        {
            base.Trigger();
            foreach (var item in produces)
            {
                Debug.Log($"Produced {item.Name}");
                Inventory.Instance.Add(item, true);
            }

            Console.Instance.AddText(
                $"Received: {produces.Aggregate("", (current, next) => current + (current.Equals("")? "" : ", ") + next.Name)}");
        }
    }
}