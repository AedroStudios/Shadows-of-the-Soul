using UnityEngine;

public class CameraFollow : MonoBehaviour {

  [Header("Variables")]
  [SerializeField] private Vector2 minPosition;
  [SerializeField] private Vector2 maxPosition;
  [Space]
  [SerializeField] private float offsetMove = 5f; 
  [Range(0,5)][SerializeField] private float offsetY = 1.5f;
  private Vector3 newPos;

  [Header("Objects")]
  private Transform target;

  private void Start()
  {
    target = FindObjectOfType<PlayerController>().transform;
  }
  private void FixedUpdate()
  {
    float posX = Mathf.Clamp((target.position.x), minPosition.x, maxPosition.x);
    float posY = Mathf.Clamp((target.position.y + offsetY), minPosition.y, maxPosition.y);

    newPos = new Vector3(posX, posY, -10);

    transform.position = Vector3.Slerp(transform.position, newPos, offsetMove * Time.fixedDeltaTime);
  }
}
