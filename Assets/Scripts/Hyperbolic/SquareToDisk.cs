using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareToDiscMapping : MonoBehaviour
{
    // Function to map from square to disc
    public Vector2 MapSquareToDisc(Vector2 squareCoords)
    {
        float x = squareCoords.x;
        float y = squareCoords.y;

        float denominator = (x * x + y * y) * (1 - x * x * y * y);
        if (Mathf.Abs(denominator) < Mathf.Epsilon) // Avoid division by zero
        {
            return Vector2.zero;
        }

        float factor = Mathf.Sqrt((x * x + y * y - 2 * x * x * y * y) / denominator);

        return new Vector2(x * factor, y * factor);
    }

    // Example usage
    void Start()
    {
        Vector2 squarePoint = new Vector2(0.5f, 0.5f); // Example point in square
        Vector2 discPoint = MapSquareToDisc(squarePoint);

        Debug.Log("Disc Coordinates: " + discPoint);
    }
}
