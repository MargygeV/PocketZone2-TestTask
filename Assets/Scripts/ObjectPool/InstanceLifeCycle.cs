using System.Collections;
using UnityEngine;

public class InstanceLifeCycle : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;
    
    private void OnEnable()
    {
        StartCoroutine(LifeCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(LifeCycle());
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);
    }
}
