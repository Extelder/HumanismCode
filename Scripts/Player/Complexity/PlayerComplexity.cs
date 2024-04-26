using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComplexity : MonoBehaviour
{
    public ComplexityLevel CurrentComplexityLevel { get; private set; }

    public void ChangeLevel(ComplexityLevel complexityLevel)
    {
        CurrentComplexityLevel = complexityLevel;
        CurrentComplexityLevel.LevelSelected();
    }
}