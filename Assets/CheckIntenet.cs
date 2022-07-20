using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckIntenet : MonoBehaviour
{
    [SerializeField] TMP_Text Loading;
    [SerializeField] TMP_Text tryAgain;
    [SerializeField] TMP_Text title;
    [SerializeField] Button tryAgainBtn;
    [SerializeField] GameObject tela;
    [SerializeField] GameObject loadinAnim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }
    private void OnEnable()
    {
        StartCoroutine(CheckInternetConnection());
    }

    IEnumerator CheckInternetConnection()
    {
        Loading.gameObject.SetActive(true);
        loadinAnim.SetActive(true);
        tryAgain.gameObject.SetActive(false);
        tryAgainBtn.gameObject.SetActive(false);
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Loading.gameObject.SetActive(false);
            loadinAnim.SetActive(false);
            tryAgain.gameObject.SetActive(true);
            tryAgainBtn.gameObject.SetActive(true);
        }
        else
        {
            tela.SetActive(false);
            InvokeRepeating(nameof(Check), 5.0f, 5.0f);
        }

    }
    public void CheckInternt()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Check()
    {
        StartCoroutine(CheckGoogle());
    }

    IEnumerator CheckGoogle()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            CancelInvoke();
            tela.SetActive(true);
            title.text = "Error";
            Loading.gameObject.SetActive(false);
            loadinAnim.SetActive(false);
            tryAgain.gameObject.SetActive(true);
            tryAgainBtn.gameObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<NewMenu>().ScreenUpdate();
        }
    }
}
