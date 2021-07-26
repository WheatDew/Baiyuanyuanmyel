using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCharacterValueManager : MonoBehaviour
{
    public Dictionary<int, CharacterValue> characterValueList = new Dictionary<int, CharacterValue>();
    public Stack<string> command = new Stack<string>();

}
