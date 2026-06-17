using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Difficulty")]
    public int difficulty = 1;
    public bool enemies = true;

    [Header("Enemy Categories")]
    public List<GameObject> regularEnemies = new();
    public List<GameObject> gimmickSet1Enemies = new();
    public List<GameObject> gimmickSet2Enemies = new();
    public List<GameObject> bigEnemies = new();

    [Header("Other Spawns")]
    public GameObject meteortype;

    [Header("Shop")]
    public GameObject shopMenu;
    public ShopUI UI;

    [Header("Ocean Current")]
    public ParticleSystem current;
    public GameObject rotateparent;

    private List<GameObject> spawnpoints;

    private const float RegularEnemyDifficultyMultiplier = 0.2f;
    private const float GimmickSet1DifficultyMultiplier = 0.15f;
    private const float GimmickSet2DifficultyMultiplier = 0.24f;
    private const float BigEnemyDifficultyMultiplier = 0.1f;
    private const float ExtraBigEnemyDifficultyMultiplier = 0.08f;

    private enum EnemySpawnCategory
    {
        Regular,
        GimmickSet1,
        GimmickSet2,
        Big
    }

    private void Start()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnRandomMeteors());
        StartCoroutine(SpawnOceanCurrent());

        ExitScreen.instance = GameObject.FindGameObjectWithTag("Exit");
    }

    private void Update()
    {
        StaticVars.wave = difficulty;
        enemies = FindObjectsOfType<EnemyBehavior>().Length > 1;
    }

    private IEnumerator SpawnEnemies()
    {
        while (CharacterController2D.Instance != null)
        {
            yield return new WaitForSeconds(1f);

            if (enemies)
                continue;

            yield return new WaitForSeconds(Random.Range(3f, 6f));

            int regularCount = GetSpawnCount(RegularEnemyDifficultyMultiplier);
            int gimmickSet1Count = GetSpawnCount(GimmickSet1DifficultyMultiplier);
            int gimmickSet2Count = 0;
            int bigEnemyCount = 0;

            if (difficulty > 4 && RandomChance())
                gimmickSet2Count = GetSpawnCount(GimmickSet2DifficultyMultiplier);

            if (difficulty > 9)
            {
                if (RandomChance())
                {
                    gimmickSet1Count = 0;
                    bigEnemyCount = GetSpawnCount(BigEnemyDifficultyMultiplier);
                }

                if (RandomChance())
                    bigEnemyCount += GetSpawnCount(ExtraBigEnemyDifficultyMultiplier);
            }

            yield return SpawnGroup(regularEnemies, regularCount, EnemySpawnCategory.Regular);
            yield return SpawnGroup(gimmickSet1Enemies, gimmickSet1Count, EnemySpawnCategory.GimmickSet1);
            yield return SpawnGroup(gimmickSet2Enemies, gimmickSet2Count, EnemySpawnCategory.GimmickSet2);
            yield return SpawnGroup(bigEnemies, bigEnemyCount, EnemySpawnCategory.Big);

            FinishWave();
        }
    }

    private IEnumerator SpawnGroup(List<GameObject> enemyList, int amount, EnemySpawnCategory category)
    {
        if (enemyList == null || enemyList.Count == 0)
            yield break;

        for (int i = 0; i < amount; i++)
        {
            GameObject spawnPoint = GetRandomSpawnPoint();

            yield return spawnPoint.GetComponent<SpawnPoint>().DisplayWarn();

            GameObject enemy = Instantiate(GetRandomEnemy(enemyList));
            ApplySpawnModifiers(enemy, category);
            enemy.transform.position = spawnPoint.transform.position;
        }
    }

    private void ApplySpawnModifiers(GameObject enemy, EnemySpawnCategory category)
    {
        switch (category)
        {
            case EnemySpawnCategory.Regular:
                if (enemy.TryGetComponent(out EnemyBehavior enemyBehavior))
                {
                    enemyBehavior.speed *= RandomMultiplier(0.8f, 1.5f);
                    enemyBehavior.turnspeed *= RandomMultiplier(0.5f, 1.5f);
                }

                enemy.transform.localScale *= RandomMultiplier(0.75f, 1.25f);
                break;

            case EnemySpawnCategory.GimmickSet1:
                if (enemy.TryGetComponent(out FastEnemyBehavior fastEnemyBehavior))
                {
                    fastEnemyBehavior.speed *= RandomMultiplier(0.8f, 1.2f);
                    fastEnemyBehavior.turnspeed *= RandomMultiplier(0.75f, 1.25f);
                }

                enemy.transform.localScale *= RandomMultiplier(0.75f, 1.25f);
                break;

            case EnemySpawnCategory.GimmickSet2:
                if (enemy.TryGetComponent(out BounceEnemyBehavior bounceEnemyBehavior))
                {
                    bounceEnemyBehavior.speed *= RandomMultiplier(0.8f, 1.2f);
                }

                if (enemy.TryGetComponent(out BubbleEnemyBehaviour bubbleEnemyBehaviour))
                {
                    bubbleEnemyBehaviour.speed *= RandomMultiplier(0.5f, 1.5f);
                    bubbleEnemyBehaviour.turnspeed *= RandomMultiplier(0.5f, 1.5f);

                    enemy.transform.localScale *= RandomMultiplier(0.75f, 1.25f);
                }

                break;

            case EnemySpawnCategory.Big:
                enemy.transform.localScale *= RandomMultiplier(0.9f, 1.1f);
                break;
        }
    }

    private IEnumerator SpawnRandomMeteors()
    {
        while (CharacterController2D.Instance != null)
        {
            yield return new WaitForSeconds(Random.Range(30f, 90f));

            GameObject spawnPoint = GetRandomSpawnPoint();

            yield return spawnPoint.GetComponent<SpawnPoint>().DisplayWarn();

            GameObject meteor = Instantiate(meteortype);

            if (meteor.TryGetComponent(out MeteorBehavior meteorBehavior))
                meteorBehavior.speed *= RandomMultiplier(0.5f, 1.5f);

            meteor.transform.localScale *= RandomMultiplier(0.2f, 1f);
            meteor.transform.position = spawnPoint.transform.position;
        }
    }

    private IEnumerator SpawnOceanCurrent()
    {
        while (CharacterController2D.Instance != null)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(Random.Range(50f, 240f));

            Quaternion direction = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            yield return new WaitForSeconds(3f);

            rotateparent.transform.rotation = direction;
            SetCurrentEmission(true);

            CharacterController2D.Instance.passivevelocity +=
                (Vector2)rotateparent.transform.right * 0.02f;

            yield return new WaitForSeconds(Random.Range(27f, 43f));

            SetCurrentEmission(false);

            // TODO: Interpolate passive velocity back to 0.
            CharacterController2D.Instance.passivevelocity = Vector2.zero;
        }
    }

    private int GetSpawnCount(float difficultyMultiplier)
    {
        return 1 + Random.Range(0, Mathf.Max(1, (int)(difficulty * difficultyMultiplier)));
    }

    private void FinishWave()
    {
        difficulty++;
        StaticVars.highscore += 5 * difficulty;

        if (difficulty % 5 == 0)
        {
            shopMenu.SetActive(true);
            UI.UpdateShop(difficulty);
            Time.timeScale = 0;
        }
    }

    private GameObject GetRandomSpawnPoint()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Count)];
    }

    private GameObject GetRandomEnemy(List<GameObject> enemies)
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    private float RandomMultiplier(float min, float max)
    {
        return Random.Range(min, max);
    }

    private bool RandomChance()
    {
        return Random.Range(0, 10) > 5;
    }

    private void SetCurrentEmission(bool enabled)
    {
        ParticleSystem.EmissionModule emission = current.emission;
        emission.enabled = enabled;
    }
}