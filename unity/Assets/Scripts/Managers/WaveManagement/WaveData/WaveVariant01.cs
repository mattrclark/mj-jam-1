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

    public class WaveVariantB : WaveVariant
    {
        private readonly IWaveContext context;
        private readonly ISpawner     spawner;

        public WaveVariantB(IWaveContext context, ISpawner spawner)
        {
            this.context = context;
            this.spawner = spawner;
        }

        public override Wave[] Waves => new[]
                                        {
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.North)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.South)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.East)
                                                        .AddEnemy(EnemyType.Drone, 1, Map01Positions.West)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1, Map01Positions.North)
                                                        .AddEnemy(EnemyType.Turret, 1, Map01Positions.South)
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.East + new Vector2(0, 1))
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.East + new Vector2(0, -1))
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.West + new Vector2(0, 1))
                                                        .AddEnemy(EnemyType.Drone, 1,
                                                                  Map01Positions.West + new Vector2(0, -1))),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.East + new Vector2(0, -6))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.East + new Vector2(0, -3))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.East + new Vector2(0, -0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.East + new Vector2(0, 3))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.East + new Vector2(0, 6))),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.West + new Vector2(0, -6))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.West + new Vector2(0, -3))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.West + new Vector2(0, -0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.West + new Vector2(0, 3))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.West + new Vector2(0, 6)))
                                        };
    }

    public class WaveVariantC : WaveVariant
    {
        private readonly IWaveContext context;
        private readonly ISpawner     spawner;

        public WaveVariantC(IWaveContext context,
                            ISpawner     spawner)
        {
            this.context = context;
            this.spawner = spawner;
        }

        public override Wave[] Waves => new[]
                                        {
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 4, Map01Positions.North)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 4, Map01Positions.South)
                                                        .AddPause(3f)
                                                        .AddEnemiesWithJitter(EnemyType.Turret, 2, Map01Positions.North)
                                                        .AddEnemiesWithJitter(EnemyType.Turret, 2,
                                                                              Map01Positions.South)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 4, Map01Positions.East)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 4, Map01Positions.West)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1, Vector2.zero)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 3, Map01Positions.East)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 3, Map01Positions.West)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 3, Map01Positions.North)
                                                        .AddEnemiesWithJitter(EnemyType.Drone, 3,
                                                                              Map01Positions.South)),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.North + new Vector2(-6, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.North + new Vector2(-3, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.North + new Vector2(0, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.North + new Vector2(3, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.North + new Vector2(6, 0))),
                                            new Wave(context,
                                                     new WaveSequence(spawner)
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.South + new Vector2(-6, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.South + new Vector2(-3, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.South + new Vector2(0, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.South + new Vector2(3, 0))
                                                        .AddEnemy(EnemyType.Turret, 1,
                                                                  Map01Positions.South + new Vector2(6, 0)))
                                        };
    }
}