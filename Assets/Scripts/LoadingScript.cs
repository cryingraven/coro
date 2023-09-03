using UnityEngine;
using System.Collections;
using admob;
public class LoadingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Admob.Instance().removeBanner();
        StartCoroutine("DoLoading");
	}
    IEnumerator DoLoading()
    {
        yield return new WaitForSeconds(3.0f);
        MoveToHome();
    }
    void MoveToHome()
    {
        Application.LoadLevel("home");
    }
}
