using UnityEngine;

public enum ShapeType { Circle, Triangle, Square }
public enum AnimalType { G, T, Y, S, C, R }

[CreateAssetMenu(menuName = "Match3/GameElement")]
public class GameElement : ScriptableObject
{
    public Sprite sprite;
    public ShapeType shape;
    public AnimalType animal;
    public Color borderColor;
}