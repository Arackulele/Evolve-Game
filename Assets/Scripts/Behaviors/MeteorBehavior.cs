using System.Collections;
using UnityEngine;

public class MeteorBehavior : MonoBehaviour
{
    public float speed = 1f;
    public float hp = 20f;

    [SerializeField] private int splitDepth = 0;
    [SerializeField] private int maxSplitDepth = -1;

    private bool dying;
    private bool shouldAimAtPlayerOnStart = true;

    private const int MinimumMaxSplitDepth = 1;
    private const int MaximumMaxSplitDepth = 2;
    private const int SplitCount = 2;

    private const float ChildHealth = 12f;
    private const float ChildScaleMultiplier = 0.45f;

    private const float ChildSpeedMultiplierMin = 0.9f;
    private const float ChildSpeedMultiplierMax = 1.1f;

    private const float Lifetime = 50f;

    private const float PlayerBounceMultiplier = -0.6f;
    private const float MinimumBounceVelocity = 2f;
    private const float BounceBoostXThreshold = 4f;
    private const float BounceBoostYThreshold = 5f;
    private const float BounceBoostMultiplier = 1.6f;

    private void Start()
    {
        if (maxSplitDepth < 0)
            maxSplitDepth = Random.Range(MinimumMaxSplitDepth, MaximumMaxSplitDepth + 1);

        if (shouldAimAtPlayerOnStart)
            AimAtPlayer();

        StartCoroutine(DestroyAfterTime());
    }

    private void Update()
    {
        Move();

        if (hp <= 0f && !dying)
            Die();
    }

    private void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void AimAtPlayer()
    {
        if (CharacterController2D.Instance == null)
            return;

        Vector2 directionToPlayer =
            CharacterController2D.Instance.transform.position - transform.position;

        transform.right = directionToPlayer.normalized;
    }

    private void Die()
    {
        dying = true;

        if (CanSplit())
            Split();

        Destroy(gameObject);
    }

    private bool CanSplit()
    {
        return splitDepth < maxSplitDepth;
    }

    private void Split()
    {
        for (int i = 0; i < SplitCount; i++)
        {
            GameObject child = Instantiate(gameObject, transform.position, GetRandomSplitRotation());
            MeteorBehavior childMeteor = child.GetComponent<MeteorBehavior>();

            childMeteor.hp = ChildHealth;
            childMeteor.speed = speed * Random.Range(ChildSpeedMultiplierMin, ChildSpeedMultiplierMax);
            childMeteor.splitDepth = splitDepth + 1;
            childMeteor.maxSplitDepth = maxSplitDepth;
            childMeteor.dying = false;
            childMeteor.shouldAimAtPlayerOnStart = false;

            child.transform.localScale = transform.localScale * ChildScaleMultiplier;
        }
    }

    private Quaternion GetRandomSplitRotation()
    {
        return Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out CharacterController2D player))
        {
            BouncePlayer(player);
            return;
        }

        if (col.TryGetComponent(out BaseBulletBehavior bullet))
        {
            TakeBulletDamage(bullet);
        }
    }

    private void BouncePlayer(CharacterController2D player)
    {
        player.velocity *= PlayerBounceMultiplier;

        if (Mathf.Abs(player.velocity.x) < MinimumBounceVelocity &&
            Mathf.Abs(player.velocity.y) < MinimumBounceVelocity)
        {
            Vector2 awayFromMeteor = player.transform.position - transform.position;
            player.velocity += awayFromMeteor.normalized;
        }

        if (Mathf.Abs(player.velocity.x) < BounceBoostXThreshold &&
            Mathf.Abs(player.velocity.y) < BounceBoostYThreshold)
        {
            player.velocity *= BounceBoostMultiplier;
        }
    }

    private void TakeBulletDamage(BaseBulletBehavior bullet)
    {
        hp -= bullet.basedamage;

        PlayBulletSound(bullet);

        if (!bullet.piercing)
            Destroy(bullet.gameObject);
    }

    private void PlayBulletSound(BaseBulletBehavior bullet)
    {
        if (bullet.Sound == null || bullet.Sound.Count == 0)
            return;

        AudioSource sound = bullet.Sound.GetRandomItem();

        if (sound != null)
            sound.PlayOneShot(bullet.Audio);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }
}