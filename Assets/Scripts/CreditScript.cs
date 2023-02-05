using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    public TMP_Text TextField;
    [TextArea]
    public List<string> CreditStrings;
    public float CharacterDelay = 0.025f;
    public float LineDelay = 0.25f;

    private readonly StringBuilder _builder = new StringBuilder();

    public void ShowCredits() => StartCoroutine(DoTextCrawl());

    private IEnumerator DoTextCrawl()
    {
        foreach (string line in CreditStrings)
        {
            int indexOfLine = CreditStrings.IndexOf(line);
            char[] charArray = line.ToCharArray();
            foreach (char c in charArray)
            {
                _builder.Append(c);
                TextField.text = _builder.ToString();
                yield return new WaitForSeconds(indexOfLine == CreditStrings.Count - 1 ? CharacterDelay * 0.25f : CharacterDelay);
            }
            _builder.Append("\n");
            yield return new WaitForSeconds(indexOfLine == CreditStrings.Count - 1 ? LineDelay * 0.25f : LineDelay);
        }
    }
}
