using System.Collections;
using UnityEngine;

namespace Managers
{
    public class PauseSpawnerItem : SpawnerItem
    {
        private readonly float time;

        public PauseSpawnerItem(float time)
        {
            this.time = time;
        }

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(time);
        }
    }
}