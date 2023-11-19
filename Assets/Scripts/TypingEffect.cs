using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public Text text;
    private string[] m_text;
    private int j;
    private IEnumerator enumerator;
    // Start is called before the first frame update
    void Start()
    {
        j = 0;
        m_text = new string[4];
        m_text[0] = "우리 부모님은 엑소 시스트이다.";
        m_text[1] = "최근 마을에 이상한 기운이 맴돌아 악령이 자주 나타나고 있다고 한다.";
        m_text[2] = "오늘도 옆 마을에 부모님께서 봉인해둔 악령이 다시 깨어나 \n 출장을 떠나셨기 때문에 집을 혼자 지키고 있어야 한다.";
        m_text[3] = "우리 집에도 위험한 악령들이 봉인되어 있는데… 뭔가 예감이 좋지 않아…";
        StartCoroutine("_typing", j);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(j);
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
            StopCoroutine("_typing");
            if(j == m_text.Length-1){
                StartCoroutine(SkipFadeIN());
            }
            else{
                j++;
                text.text = "";
                StartCoroutine("_typing", j);
            }
        }
    }

    IEnumerator _typing(int j){
        for(int i = 0; i <= m_text[j].Length; i++){
            text.text = m_text[j].Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
        if(j == m_text.Length - 1){
            StartCoroutine(SkipFadeIN());
        }
       // yield return new WaitForSeconds(2f);
    }

    public void SkipButton(){
        StopCoroutine("_typing");
        StartCoroutine(SkipFadeIN());
    }

    IEnumerator SkipFadeIN(){
        for(int i = 10; i >= 0; i--){
            float f = i / 10.0f;
            Color c = text.color;
            c.a = f;
            text.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
    }
}
