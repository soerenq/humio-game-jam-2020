using UnityEngine;
using UnityEngine.SceneManagement;

namespace Humio
{
    public class DroppableSceneNavigator : Droppable
    {
        [SerializeField] private string sceneName;

        public override void Trigger()
        {
            base.Trigger();
            SceneManager.LoadScene(sceneName);
        }
    }
}