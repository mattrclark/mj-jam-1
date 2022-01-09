using Entities.AttackableEntities.Enemies;
using UnityEngine;
using Values.MapPositions;

namespace Managers.WaveManagement.WaveData
{
    public abstract class WaveVariant
    {
        public abstract Wave[] Waves { get; }
    }


    public class WaveVariantA : WaveVariant
    {
        private readonly IWaveContext context;
        private readonly ISpawner     spawner;

        public WaveVariantA(IWaveContext context, ISpawner spawner)
        {
            this.context = context;
            this.spawner = spawner;
        }

        public override Wave[] Waves => new[]
                                        {
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.North)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.North)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.South)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1, Map01Positions.North)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.North)
                                                        .AddPause(0.2f)
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(-1, 0))
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(1, 0))
                                                        .AddPause(0.2f)
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(-2, 0))
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(2, 0))),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1, Map01Positions.North)
                                                        .AddPause(1f)
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(-1, 0))
                                                        .AddPause(0.3f)
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(1, 0))
                                                        .AddPause(0.3f)
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(-2, 0))
                                                        .AddPause(0.3f)
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.North + new Vector2(2, 0)))
                                        };
    }
}