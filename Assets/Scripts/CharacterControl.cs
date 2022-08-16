using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityStandardAssets.CrossPlatformInput;  [RequireComponent(typeof(Rigidbody))] [RequireComponent(typeof(Animator))]  public class CharacterControl : MonoBehaviour {      [SerializeField]     int moveSpeed;     [SerializeField]     int jumpHeight;     [SerializeField]     float range = 10f;      private Rigidbody charRigidBody;     private Animator anim;  	// Use this for initialization 	void Start () {         charRigidBody = GetComponent<Rigidbody>();         anim = GetComponent<Animator>(); 	} 	 	// Update is called once per frame 	void Update ()     {         CharacterMovement();     }      private void CharacterMovement()     {         Vector3 moveChar = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));         transform.position += moveChar * Time.deltaTime * moveSpeed;          if (charRigidBody.velocity.magnitude == 0)         {             anim.SetBool("isWalking", false);         }         else         {             anim.SetBool("isWalking", true);         }          if (GameManager.instance.IsJumping)
        {
            anim.SetTrigger("Jump");
            transform.Translate(Vector3.up * jumpHeight * Time.deltaTime, Space.World);
            GameManager.instance.IsJumping = false;
        }          if (GameManager.instance.IsPunching)
        {
            anim.SetTrigger("Attack");
            ModifyTerrain.instance.DestroyBlock(range, (byte)TextureType.air.GetHashCode());
            GameManager.instance.IsPunching = false;
        }

        if (GameManager.instance.IsBuilding)
        {
            anim.SetTrigger("Attack");
            ModifyTerrain.instance.AddBlock(range, (byte)TextureType.rock.GetHashCode());
            GameManager.instance.IsBuilding = false;
        }      } } 