using System.Collections;

namespace Managers.WaveManagement
{
    public abstract class SpawnerItem
    {
        public abstract IEnumerator Execute();
    }
}