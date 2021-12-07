using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlitchyFont : MonoBehaviour
{
    public TMP_FontAsset normalFont;
    public TMP_FontAsset alienFont;
    private TMP_Text textComponent;

    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        timeRemaining = Random.Range(0, 3f);
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(Glitch());
        }
    }

    private IEnumerator Glitch()
    {
        textComponent.font = alienFont;
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
        textComponent.font = normalFont;
        timeRemaining = Random.Range(0, 5f);
    }
}
