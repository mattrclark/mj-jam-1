using System.Linq;

namespace Managers.WaveManagement
{
    public class Wave
    {
        private readonly IWaveContext      context;
        private readonly SpawnerSequence[] sequences;

        public Wave(IWaveContext context, params SpawnerSequence[] sequences)
        {
            this.context   = context;
            this.sequences = sequences;
        }

        public int TotalEnemies => sequences.Sum(s => s.TotalEnemies);

        public void InitiateSequence()
        {
            foreach (var sequence in sequences)
                context.StartCoroutine(sequence.Execute());
        }
    }
}