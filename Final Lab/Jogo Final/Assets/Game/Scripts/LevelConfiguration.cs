using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Runner Game/LevelConfiguration", order = 1)] //crio um menu 

/// <summary>
/// Responsavel pela configuração de cada level
/// </summary>
public class LevelConfiguration : ScriptableObject // defino como uma classe publica
{
    public float speed; // defino speed como uma class float 
    public float minRangeObstacleGenerator;// defino minRangeObstacleGenerator como uma classe float 
    public float maxRangeObstacleGenerator;// defino  maxRangeObstacleGenerator como uma classe float 
    public int minScore;// defino minScore como uma classe int
}
