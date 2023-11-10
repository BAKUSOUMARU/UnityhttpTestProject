using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RequestTest : MonoBehaviour
{
    private string csrfToken;

    private void Start()
    {
        StartCoroutine(GetCsrfTokenAndRegisterUser()); // WebTestをコルーチンとして呼び出す
    }

    IEnumerator GetCsrfTokenAndRegisterUser()
    {
        Debug.Log("CSRFトークンを取得中");

        // CSRFトークンを取得するエンドポイント
        string csrfTokenUrl = "http://bakusoumaru.site:3000/csrf-token-endpoint";

        UnityWebRequest tokenRequest = UnityWebRequest.Get(csrfTokenUrl);
        yield return tokenRequest.SendWebRequest();

        if (tokenRequest.result == UnityWebRequest.Result.Success)
        {
            csrfToken = tokenRequest.downloadHandler.text;
            Debug.Log("CSRFトークンを取得しました: " + csrfToken);

            // ユーザーの登録
            string registerUrl = "http://bakusoumaru.site:3000/api/register";
            WWWForm form = new WWWForm();
            form.AddField("username", "bakusoumaru");
            form.AddField("password", "1234");

            UnityWebRequest registerRequest = UnityWebRequest.Post(registerUrl, form);
            
            // リクエストヘッダーにCSRFトークンを追加
            registerRequest.SetRequestHeader("X-CSRF-Token", csrfToken);

            yield return registerRequest.SendWebRequest();

            if (registerRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("ユーザーが正常に登録されました");
            }
            else
            {
                Debug.Log("ユーザー登録に失敗しました：" + registerRequest.error);
            }
        }
        else
        {
            Debug.Log("CSRFトークンの取得に失敗しました：" + tokenRequest.error);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 何か追加の処理を行う場合にはここに記述
    }
}