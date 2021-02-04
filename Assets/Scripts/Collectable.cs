using System.Collections;
using Managers;
using UnityEngine;

public class Collectable : MonoBehaviour {
    [SerializeField] private float collectTime = 3f;

    private Transform player;
    private const float CollectingDistance = 0.1f;

    private void Start() {
        player = GameManager.Instance.GetPlayer();
    }

    public void Collect() {
        Collider collider = GetComponent<Collider>();
        if (collider) collider.enabled = false;

        StartCoroutine(Collect(collectTime));
    }

    private IEnumerator Collect(float delay) {
        float time = 0;

        while (time < delay && Vector3.Distance(transform.position, player.position) > CollectingDistance) {
            transform.position = Vector3.Lerp(transform.position, player.position, time / delay);
            yield return null;
            time += Time.deltaTime;
        }

        Destroy(gameObject);
    }
}