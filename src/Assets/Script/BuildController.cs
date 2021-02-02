using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildController : MonoBehaviour
{
    public RuleTile grassTile;
    public Tilemap groundTileMap;

    public float castDistance = 1.0f;
    public Transform raycastPoint;
    public LayerMask layer;

    float blockDestroyTime = 0f;
    Vector3 Direction;
    RaycastHit2D hit;

    bool destroyingBlock = false;
    bool placingBlock = false;

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.C))
        {
            RaycastDirection();
        }
    }


    void RaycastDirection()
    {
        if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0)
        {
            Direction.x = Input.GetAxis("Horizontal");
            Direction.y = Input.GetAxis("Vertical");
        }

        hit = Physics2D.Raycast(raycastPoint.position, Direction, castDistance, layer.value);

        Vector2 endpos = raycastPoint.position + Direction;

        Debug.DrawLine(raycastPoint.position, endpos, Color.red);

    

        if (Input.GetKey(KeyCode.F))
        {
            if(hit.collider && !destroyingBlock)
            {
                destroyingBlock = true;
                StartCoroutine(DestroyBlock(hit.collider.gameObject.GetComponent<Tilemap>(), endpos));
            }
        }

        if(Input.GetKey(KeyCode.C))
        {
            if(!hit.collider && !placingBlock)
            {
                placingBlock = true;
                StartCoroutine(PlaceBlock(groundTileMap, endpos));
            }
        }
    }

    IEnumerator DestroyBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(blockDestroyTime);

        pos.y = Mathf.Floor(pos.y);
        pos.x = Mathf.Floor(pos.x);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), null);

        destroyingBlock = false;
    }

    IEnumerator PlaceBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(0f);

        pos.y = Mathf.Floor(pos.y);
        pos.x = Mathf.Floor(pos.x);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), grassTile);

        placingBlock = false;
    }








}
