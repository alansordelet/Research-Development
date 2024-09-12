using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public class HyperbolicTileMapGenerator : MonoBehaviour
//{
//    public GameObject tilePrefab;
//    public GameObject pointPrefab;
//    public float tileRadius = 0.1f;
//    public int p = 4; // Number of sides of the tile (squares)
//    private int q = 5; // Number of tiles meeting at each vertex
//    private List<GameObject> placedTiles = new List<GameObject>();

//    void Start()
//    {
//        if (!CheckHyperbolicTilingFeasibility(p, q))
//        {
//            Debug.LogError("Hyperbolic tiling not feasible with p = " + p + " and q = " + q);
//            return;
//        }

//        GenerateTile(Vector3.zero, 0); // Start generation from the center
//    }

//    void GenerateTile(Vector3 position, int depth)
//    {
//        if (depth > 5 || IsTilePlaced(position)) return;

//        GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
//        placedTiles.Add(newTile);
//        ScaleTile(newTile, position);

//        for (int i = 0; i < p; i++)
//        {
//            Vector3 newPosition = CalculateNextTilePosition(position, i);
//            GenerateTile(newPosition, depth + 1);
//        }
//    }

//    bool IsTilePlaced(Vector3 position)
//    {
//        foreach (GameObject tile in placedTiles)
//        {
//            if (Vector3.Distance(tile.transform.position, position) < tileRadius) return true;
//        }
//        return false;
//    }

//    Vector3 CalculateNextTilePosition(Vector3 currentPosition, int directionIndex)
//    {
//        float angle = 2 * Mathf.PI * directionIndex / p;
//        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
//        return currentPosition + direction.normalized * tileRadius * 2;
//    }

//    void ScaleTile(GameObject tile, Vector3 position)
//    {
//        Complex z = new Complex(position.x, position.z);
//    Complex transformed = TransformToHyperbolicDisc(z);
//    float scale = Mathf.Max(1f - transformed.Abs(), 0.1f); // Use the Abs method to get the magnitude
//    tile.transform.localScale = Vector3.one * scale;
//    }

//    Complex TransformToHyperbolicDisc(Complex z)
//    {
//        Complex i = new Complex(0, 1);
//        return (z - i) / (z + i);
//    }

//    bool CheckHyperbolicTilingFeasibility(int p, int q)
//    {
//        return (p - 2) * (q - 2) > 4;
//    }

//    // Additional methods for visualizing points, drawing lines, etc., can be added here
//}
public class HyperbolicTileMapGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Assign your square tile prefab
    [SerializeField] public GameObject pointPrefab; // Assign your square tile prefab
    public float tileRadius = 0.1f; // Radius of each tile
    private List<GameObject> placedTiles = new List<GameObject>(); // List to keep track of placed tiles

    // Tiling parameters
    public int p = 4; // Number of sides of the tile (squares)
    private int q = 5; // Number of tiles meeting at each vertex, to be defined
    public Transform player;


    void Start()
    {
        if (!CheckHyperbolicTilingFeasibility(p, q))
        {
            Debug.LogError("Hyperbolic tiling not feasible with p = " + p + " and q = " + q);
            return;
        }

        //GenerateTile(Vector3.one, 0); // Start generation from the center



        float bdist = 10.0f; // Example values
        float brot = 45.0f;  // Example values

        List<Vector2> positions = CreateHyperbolicPolygon(bdist, brot);
        CreateEditablePoints(positions);

        //List<Vector2> positions = CreateHyperbolicPolygon(bdist, brot);
        //CreatePoints(positions);
    }

    void GenerateTile(Vector3 position, int depth)
    {
        if (depth > 5) // Limit the depth to prevent infinite recursion
        {
            return;
        }

        Vector2 discPosition = MapSquareToDisc(new Vector2(position.x, position.z));

        if (IsTilePlaced(discPosition))
        {
            return;
        }

        GameObject newTile = Instantiate(tilePrefab, new Vector3(discPosition.x, 0, discPosition.y), Quaternion.identity);
        placedTiles.Add(newTile);

        // Adjust the scale based on distance from the edge of the unit disc
        float distanceFromEdge = 1.0f - discPosition.magnitude; // Distance from the edge of the unit disc
        distanceFromEdge = Mathf.Clamp(distanceFromEdge, 0.01f, 1.0f); // Clamp to ensure it's within the unit disc
        newTile.transform.localScale = Vector3.one * Mathf.Max(distanceFromEdge, 0.1f);

        for (int i = 0; i < p; i++)
        {
            Vector3 newPosition = CalculateNextTilePosition(position, i);
            GenerateTile(newPosition, depth + 1);
        }
    }

    bool IsTilePlaced(Vector2 position)
    {
        foreach (GameObject tile in placedTiles)
        {
            Vector3 tilePos = tile.transform.position;
            if (Vector2.Distance(new Vector2(tilePos.x, tilePos.z), position) < tileRadius)
                return true;
        }
        return false;
    }

    Vector3 CalculateNextTilePosition(Vector3 currentPosition, int directionIndex)
    {
        // This is a simplified version. In actual hyperbolic geometry,
        // you would calculate the next position based on hyperbolic rules.
        // For now, let's just spread them in a basic grid pattern.
        float angle = 2 * Mathf.PI * directionIndex / p;
        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        return currentPosition + direction.normalized * tileRadius * 2;
    }


    bool CheckHyperbolicTilingFeasibility(int p, int q)
    {
        return (p - 2) * (q - 2) > 4;
    }

    Vector2 MapSquareToDisc(Vector2 squareCoords)
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
 
    Complex TransformToHyperbolicDisc(Complex z)
    {
        Complex i = new Complex(0, 1);
        return (z - i) / (z + i);
    }
    void ScaleTile(GameObject tile, Vector3 position)
    {
        Complex z = new Complex(position.x, position.z);
        Complex transformed = TransformToHyperbolicDisc(z);
        float scale = Mathf.Max(1f - transformed.Abs(), 0.1f);
        tile.transform.localScale = Vector3.one * scale;
    }
    private float CalculateHyperbolicDistance(Vector2 pointP, Vector2 pointQ)
    {
        float x1 = pointP.x, y1 = pointP.y;
        float x2 = pointQ.x, y2 = pointQ.y;

        if ((x1 * x1 + y1 * y1 >= 1) || (x2 * x2 + y2 * y2 >= 1))
        {
            Debug.LogError("Points must be inside the unit disc for hyperbolic distance calculation");
            return 0;
        }

        float u = Mathf.Pow(1 - x1 * x2 - y1 * y2, 2) + Mathf.Pow(x1 * y2 - x2 * y1, 2);
        float v = Mathf.Pow(x1 - x2, 2) + Mathf.Pow(y1 - y2, 2);

        if (u - v < 0)
        {
            Debug.LogError("Invalid points for hyperbolic distance calculation");
            return 0;
        }

        return Mathf.Log(u + Mathf.Sqrt(u - v));
    }

    Vector2 MoebiusInverse(Vector2 o, float r, Vector3 p)
    {
        Complex pComplex = new Complex(p.x, p.y);
        Complex c = pComplex / (r * r);

        Complex oComplex = new Complex(o.x, o.y);

        Complex a = Complex.One;
        Complex b = Complex.Zero;
        Complex d = Complex.One;

        Complex inverse = (d * oComplex - b) / (-c * oComplex + a);

        return new Vector2(inverse.Real, inverse.Imaginary);
    }

    Vector2 vec3to2(Vector3 a)
    {
        return new Vector2(a.x, a.z);
    }

    Vector2 vec2to3(Vector2 a)
    {
        return new Vector3(a.x, 0, a.y);
    }

    public void CalculateHyperbolicCircle(Vector2 a, Vector2 b, out Vector2 ocen, out float orad)
    {
        Vector2 a2 = MoebiusInverse(Vector2.zero, 1.0f, a);
        Vector2 cpos = (a + a2) * 0.5f;
        float crad = Vector2.Distance(a, cpos);
        Vector2 b2 = MoebiusInverse(cpos, crad, b);
        Vector2 mid = (b + b2) * 0.5f;

        if (a.x == 0 && a.y == 0)
        {
            ocen = Vector2.zero;
            orad = b.magnitude;
        }
        else
        {
            ocen = mid;
            orad = Vector2.Distance(mid, b);
        }

    }
    public float rotate_angle;

    public float d = 7;

    void CreatePoints(List<Vector2> positions)
    {
        foreach (Vector2 pos in positions)
        {
            Instantiate(pointPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
        }
    }

    void CalculateD()
    {
        d = Mathf.Sqrt((Mathf.Tan(Mathf.PI / 2.0f - Mathf.PI / q) - Mathf.Tan(Mathf.PI / p)) /
                       (Mathf.Tan(Mathf.PI / 2.0f - Mathf.PI / q) + Mathf.Tan(Mathf.PI / p)));
        // Assuming f@d is some kind of field or property that needs to be set
        // Here, we'll just print it for demonstration
        Debug.Log("Calculated d: " + d);

        // Using rotate_angle
        float rotang = rotate_angle;
        // Do something with rotang if needed
    }

    List<Vector2> CreateHyperbolicPolygon(float bdist, float brot)
    {
        // Calculate initial position
        Vector3 bpos = Quaternion.Euler(1, brot, 1) * new Vector3(bdist, 1, 1);

        // Create hyper circles
        Vector2 ccen = Vector2.one, dcen = Vector2.one;
        float crad = 0, drad = 0;
        CalculateHyperbolicCircle(vec3to2(bpos), Vector2.one, out ccen, out crad);
        CalculateHyperbolicCircle(Vector2.one, vec3to2(bpos), out dcen, out drad);

        // Hyperbolic bisect line
        Vector2 ip1, ip2;
        // Assuming CircleIntersection is implemented to find intersection points
        CalculateCircleIntersection(ccen, dcen, crad, drad, out ip1, out ip2);

        // Invert intersection point and find circle
        Vector2 invip = MoebiusInverse(Vector2.one, 1.0f, ip1);
        Vector2 ipo;
        float iprad;
        CalculateCircleFromThreePoints(ip1, ip2, invip, out ipo, out iprad);

        // Generate polygon points
        List<Vector2> positions = new List<Vector2>();
        for (int i = 0; i < p; i++)
        {
            float angle = 2 * Mathf.PI / p * i + Mathf.Deg2Rad * rotate_angle;
            Vector2 pos = new Vector2(d * Mathf.Cos(angle), d * Mathf.Sin(angle));
            if (bdist != 0)
            {
                pos = MoebiusInverse(ipo, iprad, pos);
            }
            Vector2 inv = MoebiusInverse(Vector2.zero, 1.0f, pos);
            positions.Add(pos);
            // Optionally store 'inv' if needed
        }
        Debug.Log("Number of positions calculated: " + positions.Count);
        //Debug.Log("Creating Hyperbolic Polygon with distance: " + distance + " and rotation angle: " + rotateAngle);
        return positions;

    }

    //void OnDrawGizmos()
    //{
    //    if (polygonPoints == null || polygonPoints.Length < 2)
    //    {
    //        return;
    //    }

    //    Gizmos.color = Color.red;

    //    for (int i = 0; i < polygonPoints.Length; i++)
    //    {
    //        // Draw points
    //        Gizmos.DrawSphere(polygonPoints[i].position, 0.1f);

    //        // Draw lines
    //        if (i > 0)
    //        {
    //            DrawHyperbolicLine(polygonPoints[i - 1].position, polygonPoints[i].position);
    //        }
    //    }

    //    // Connect the last point to the first
    //    DrawHyperbolicLine(polygonPoints[polygonPoints.Length - 1].position, polygonPoints[0].position);
    //}

    void DrawHyperbolicLine(Vector3 start, Vector3 end)
    {
        int segments = 20; // Increase for smoother lines
        for (int i = 0; i < segments; i++)
        {
            float t1 = (float)i / segments;
            float t2 = (float)(i + 1) / segments;

            Vector3 interpolatedStart = HyperbolicInterpolate(start, end, t1);
            Vector3 interpolatedEnd = HyperbolicInterpolate(start, end, t2);

            Gizmos.DrawLine(interpolatedStart, interpolatedEnd);
        }
    }

    Vector3 HyperbolicInterpolate(Vector3 start, Vector3 end, float t)
    {
        // Linear interpolation in Euclidean space
        Vector3 euclideanInterpolated = Vector3.Lerp(start, end, t);

        // Project the interpolated point onto the Poincaré disk
        Vector2 poincareInterpolated = ProjectToPoincareDisk(euclideanInterpolated);

        // Convert back to Vector3 for Unity compatibility, assuming z = 0
        return new Vector3(poincareInterpolated.x, poincareInterpolated.y, 0);
    }

    Vector2 ProjectToPoincareDisk(Vector3 point)
    {
        // Assuming the point is in the Euclidean plane, normalize if outside the unit circle
        float magnitude = Mathf.Sqrt(point.x * point.x + point.y * point.y);
        if (magnitude > 1)
        {
            return new Vector2(point.x / magnitude, point.y / magnitude);
        }
        return new Vector2(point.x, point.y);
    }

    //void OnDrawGizmos()
    //{
    //    if (polygonPoints == null || polygonPoints.Length == 0)
    //    {
    //        return;
    //    }

    //    Gizmos.color = Color.red;
    //    for (int i = 0; i < polygonPoints.Length; i++)
    //    {
    //        Gizmos.DrawSphere(polygonPoints[i].position, 0.1f); // Draw points as small spheres

    //        // Draw lines between points
    //        if (i > 0)
    //        {
    //            Gizmos.DrawLine(polygonPoints[i - 1].position, polygonPoints[i].position);
    //        }
    //    }

    //    // Connect the last point to the first
    //    if (polygonPoints.Length > 1)
    //    {
    //        Gizmos.DrawLine(polygonPoints[polygonPoints.Length - 1].position, polygonPoints[0].position);
    //    }
    //}


    public Transform[] polygonPoints;

    void CreateEditablePoints(List<Vector2> positions)
    {
        polygonPoints = new Transform[positions.Count];
        for (int i = 0; i < positions.Count; i++)
        {
            GameObject pointObject = Instantiate(pointPrefab, new Vector3(positions[i].x, 0, positions[i].y), Quaternion.identity);
            polygonPoints[i] = pointObject.transform;
        }
    }

    public void CalculateCircleFromThreePoints(Vector2 a, Vector2 b, Vector2 c, out Vector2 center, out float radius)
    {
        Vector2 ta = a;
        Vector2 tb = b;
        Vector2 tc = c;

        if (a.x == b.x || a.y == b.y)
        {
            tc = b;
            tb = c;
        }

        float ma = (tb.y - ta.y) / (tb.x - ta.x);
        float mb = (tc.y - tb.y) / (tc.x - tb.x);

        float x = (ma * mb * (ta.y - tc.y) + mb * (ta.x + tb.x) - ma * (tb.x + tc.x)) / (2 * (mb - ma));
        float y = -1.0f / ma * (x - (ta.x + tb.x) / 2.0f) + (ta.y + tb.y) / 2.0f;

        center = new Vector2(x, y);
        radius = Vector2.Distance(center, a);
    }

    public Vector2 CalculateCircleIntersection(Vector2 o1, Vector2 o2, float rad1, float rad2, out Vector2 p1, out Vector2 p2)
    {
        float d = Vector2.Distance(o1, o2);
        float a = (rad1 * rad1 - rad2 * rad2 + d * d) / (2 * d);
        Vector2 p = o1 + (o2 - o1) * a / d;
        float h = Mathf.Sqrt(rad1 * rad1 - a * a);

        p1 = new Vector2(p.x + h * (o2.y - o1.y) / d, p.y - h * (o2.x - o1.x) / d);
        p2 = new Vector2(p.x - h * (o2.y - o1.y) / d, p.y + h * (o2.x - o1.x) / d);

        return p; // Return center point of the intersection line
    }
    //void Start()
    //{
    //    q = DetermineQValue(); // Determine a suitable q value for square tiles

    //    if (CheckHyperbolicTilingFeasibility(p, q))
    //    {
    //        GenerateTile(Vector3.zero, 0); // Start generation from the center
    //    }
    //    else
    //    {
    //        Debug.LogError("Hyperbolic tiling not feasible with p = " + p + " and q = " + q);
    //    }
    //}

    //void Update()
    //{
    //    foreach (GameObject tile in placedTiles)
    //    {
    //        float scale = CalculateScaleBasedOnPlayerDistance(tile.transform.position, player.position);
    //        tile.transform.localScale = new Vector3(scale, tile.transform.localScale.y, scale);
    //    }
    //}

    //float CalculateScaleBasedOnPlayerDistance(Vector3 tilePosition, Vector3 playerPosition)
    //{
    //    // Calculate the distance between the player and the tile
    //    float distance = Vector3.Distance(tilePosition, playerPosition);

    //    // Use an appropriate formula to calculate scale based on distance
    //    // This is a simple example; you might need to adjust the formula
    //    return Mathf.Max(5 / distance, 0.1f); // Ensure a minimum scale
    //}

    //void GenerateTile(Vector3 position, int depth)
    //{
    //    if (depth > 5) // Limit the depth to prevent infinite recursion
    //    {
    //        Debug.Log("Reached maximum depth");
    //        return;
    //    }

    //    if (IsTilePlaced(position))
    //    {
    //        Debug.Log($"Tile already placed at position {position}");
    //        return;
    //    }

    //    Debug.Log("Generating tile " + placedTiles.Count+ " at depth " + depth + " and position " + position);




    //    //Complex z = new Complex(position.x, position.y);
    //    //Complex transformed = TransformToHyperbolicDisc(z);
    //    //Vector3 newPosition = new Vector3(transformed.Real, position.y , transformed.Imaginary);

    //    //GameObject newTile = Instantiate(tilePrefab, newPosition, Quaternion.identity);
    //    //placedTiles.Add(newTile);

    //    //GenerateInDirections(newPosition, depth);





    //    Complex z = new Complex(position.x, position.y); // assuming x, y are the real and imaginary parts

    //    // Apply the transformation
    //    Complex transformed = TransformToHyperbolicDisc(z);

    //    // Convert back to Vector3
    //    Vector3 newPosition = new Vector3(transformed.Real, transformed.Imaginary, position.z);

    //    GameObject newTile = Instantiate(tilePrefab, newPosition, Quaternion.identity);
    //    placedTiles.Add(newTile);

    //    // Scale the tile based on depth
    //    //  float scale = CalculateScaleFactor(depth);
    //    //newTile.transform.localScale = new Vector3(scale, newTile.transform.localScale.y, scale);

    //    // Generate tiles in all directions
    //    GenerateInDirections(position, depth);
    //}
    //float CalculateScaleFactor(int depth)
    //{
    //    // Decrease scale as depth increases
    //    // Experiment with these values to get the desired effect
    //    return Mathf.Max(5.0f / (depth + 1), 0.1f); // Ensuring the scale doesn't go below a minimum value
    //}

    //Vector3 TransformPosition(Vector3 currentPosition, Vector3 direction, int depth, int p, int q)
    //{     
    //    Vector3 newPosition = currentPosition + direction.normalized * 2.5f;
    //    return newPosition;
    //}

    //Complex TransformToHyperbolicDisc(Complex z)
    //{
    //    Complex i = new Complex(0, 1);
    //    return (z - i) / (z + i);
    //}

    //void GenerateInDirections(Vector3 position, int depth)
    //{
    //    Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
    //    //foreach (Vector3 direction in directions)
    //    //{
    //    //    Vector3 newDirection = CalculateNextDirection(position, direction, depth);
    //    //    Vector3 newPosition = TransformPositionHyperbolically(position, newDirection, depth);
    //    //    // Generate tiles with new positions
    //    //}
    //    foreach (Vector3 direction in directions)
    //    {
    //        Vector3 newDirection = CalculateNextDirection(position, direction, depth);
    //        Vector3 newPosition = TransformPosition(position, newDirection, depth, p, q);
    //        if (!IsTilePlaced(newPosition))
    //        {

    //            GenerateTile(newPosition, depth + 1);
    //        }
    //        else
    //        {
    //            Debug.Log($"Tile already placed at position {position}");
    //        }
    //    }
    //}
    //bool IsTilePlaced(Vector3 position)
    //{
    //    float threshold = tileRadius/2; // Assuming tiles are spaced by at least their diameter
    //    foreach (GameObject tile in placedTiles)
    //    {
    //        Debug.Log("Distance = " + Vector3.Distance(tile.transform.position, position));
    //        if (Vector3.Distance(tile.transform.position, position) < 2.5f)
    //            return true;
    //    }
    //    return false;
    //}

    //Vector3 TransformPositionHyperbolically(Vector3 currentPosition, Vector3 direction, int depth)
    //{
    //    Complex z = new Complex(currentPosition.x, currentPosition.y); // Convert current position to complex
    //    Complex dir = new Complex(direction.x, direction.y); // Convert direction to complex

    //    // Apply Möbius transformation or other hyperbolic transformation
    //    Complex transformedZ = HyperbolicTransformation(z, dir, depth);

    //    // Convert back to Vector3
    //    return new Vector3((float)transformedZ.Real, (float)transformedZ.Imaginary, currentPosition.z);
    //}

    //Complex HyperbolicTransformation(Complex z, Complex direction, int depth)
    //{

    //    // Scaling factor: increases with depth, but ensures the point stays within the unit disk
    //    float scale = 1f - 0.1f * depth; // Decrease scale with depth
    //    scale = Mathf.Min(scale, 0.9f); // Ensure the scale factor stays < 1 for the Poincaré disk

    //    float rotationAngle = Mathf.PI / 6f * depth; //Rotate by 30 degrees for each depth level

    //    // Define transformation coefficients
    //   Complex a = new Complex(Mathf.Cos(rotationAngle) * scale, Mathf.Sin(rotationAngle) * scale); // Scale and rotate // Scale and rotate
    //    Complex b = new Complex(0, 0); // Equivalent to Complex.Zero
    //    Complex c = new Complex(0, 0); // Equivalent to Complex.Zero
    //    Complex d = new Complex(1, 0); // Equivalent to Complex.One

    //    // Apply Möbius transformation
    //    return (a * z + b) / (c * z + d);
    //}
    //float CalculateHyperbolicDistance(int p, int q)
    //{
    //    float tanPiOverP = Mathf.Tan(Mathf.PI / p);
    //    float tanPiOver2MinusPiOverQ = Mathf.Tan(Mathf.PI / 2 - Mathf.PI / q);

    //    float numerator = tanPiOver2MinusPiOverQ - tanPiOverP;
    //    float denominator = tanPiOver2MinusPiOverQ + tanPiOverP;

    //    // Ensure the denominator is not zero to avoid division by zero
    //    if (Mathf.Abs(denominator) < Mathf.Epsilon)
    //    {
    //        Debug.LogError("Denominator is too close to zero in CalculateHyperbolicDistance");
    //        return 0f;
    //    }

    //    return Mathf.Sqrt(numerator / denominator);
    //    //return 1.0f;
    //}

    //float CalculateScaleForDepth(int depth)
    //{
    //    // This formula should decrease the scale of tiles as the depth increases
    //    // You may need to adjust the base and exponent to suit your specific needs
    //    float baseScale = 1.0f; // Starting scale at depth 0
    //    float scaleDecreaseRate = 0.9f; // Rate at which the scale decreases with each depth level

    //    // Calculate the new scale based on depth
    //    float newScale = baseScale * Mathf.Pow(scaleDecreaseRate, depth);

    //    // Ensure that the scale doesn't go below a certain minimum value
    //    float minScale = 0.1f; // Minimum scale for tiles
    //    return Mathf.Max(newScale, minScale);
    //}


    //Vector3 AdjustDirectionForDepth(Vector3 originalDirection, int depth)
    //{
    //    // Calculate a rotation angle based on the depth
    //    float rotationAngle = CalculateRotationAngle(depth);

    //    // Rotate the original direction vector by this angle
    //    Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
    //    Vector3 adjustedDirection = rotation * originalDirection;

    //    return adjustedDirection;
    //}
    //Vector3 CalculateNextDirection(Vector3 currentPosition, Vector3 currentDirection, int depth)
    //{
    //    float rotationAngle = CalculateRotationAngle(depth);
    //    Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
    //    return rotation * currentDirection;
    //}


    //float CalculateRotationAngle(int depth)
    //{
    //    // This is a placeholder function for calculating the rotation angle
    //    // The logic here should be adjusted based on how you want to simulate hyperbolic curvature
    //    // As a starting point, let's increase the angle as the depth increases
    //    return 90f * depth; // Adjust this value as needed // Example: Rotate 20 degrees more for each level of depth
    //}

    //bool CheckHyperbolicTilingFeasibility(int p, int q)
    //{
    //    return (p - 2) * (q - 2) > 4;
    //}

    //int DetermineQValue()
    //{
    //    // Determine a suitable q value for square tiles in hyperbolic geometry
    //    // This is a placeholder function and needs proper logic based on your design
    //    return 5; // Example value, adjust as needed
    //}
}
