
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
public class SpriteColliderUpdater : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private Sprite currentSprite;

    // Caches the physics shapes to avoid performance overhead.
    private Dictionary<Sprite, List<Vector2[]>> spritePhysicsShapes = new Dictionary<Sprite, List<Vector2[]>>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void LateUpdate()
    {
        // Update the collider only when the sprite has changed.
        if (spriteRenderer.sprite != currentSprite)
        {
            currentSprite = spriteRenderer.sprite;
            UpdateCollider();
        }
    }

    private void UpdateCollider()
    {
        if (currentSprite == null)
        {
            polygonCollider.pathCount = 0;
            return;
        }

        // Get and cache the physics shape from the sprite.
        if (!spritePhysicsShapes.ContainsKey(currentSprite))
        {
            int shapeCount = currentSprite.GetPhysicsShapeCount();
            var shapes = new List<Vector2[]>(shapeCount);
            for (int i = 0; i < shapeCount; i++)
            {
                var path = new List<Vector2>();
                currentSprite.GetPhysicsShape(i, path);
                shapes.Add(path.ToArray());
            }
            spritePhysicsShapes[currentSprite] = shapes;
        }

        // Apply the cached shape to the PolygonCollider2D.
        polygonCollider.pathCount = spritePhysicsShapes[currentSprite].Count;
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            polygonCollider.SetPath(i, spritePhysicsShapes[currentSprite][i]);
        }
    }
}
