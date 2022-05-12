using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller { get; private set; }
    public Transform groundCheck;

    public float speed = 12f;
    public float gravity = -9.81f;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Footsteps")]
    public string footstepSoundName;
    public LayerMask footstepMask;

    private enum CURRENT_TERRAIN { Grass, Concrete };

    private CURRENT_TERRAIN currentTerrain;

    Vector3 velocity;
    bool isGrounded;

    FMOD.Studio.EventInstance footsteps;

    bool soundPlaying = false;

    Vector3 lastFramePos;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastFramePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ActivityManager.Instance.menuOpen) { return; }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (soundPlaying)
        {
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 2f, footstepMask))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
                {
                    currentTerrain = CURRENT_TERRAIN.Grass;
                }
                else
                {
                    currentTerrain = CURRENT_TERRAIN.Concrete;
                }
                if(soundPlaying)
                {
                    SetParams();
                }
            }
        }

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        move.Normalize();

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        float currentSpeed = Vector3.Distance(lastFramePos, transform.position) / Time.deltaTime;

        if(currentSpeed > 2 && !soundPlaying)
        {
            PlayFootsteps();
        }
        else if(currentSpeed < 1 && soundPlaying)
        {
            StopFootsteps();
        }

        lastFramePos = transform.position;
    }

    void PlayFootsteps()
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance(footstepSoundName);
        soundPlaying = true;
        SetParams();
        footsteps.start();
    }

    void StopFootsteps()
    {
        footsteps.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundPlaying = false;
    }

    void SetParams()
    {
        if (currentTerrain == CURRENT_TERRAIN.Concrete)
        {
            footsteps.setParameterByName("Concrete", 1f);
        }
        else
        {
            footsteps.setParameterByName("Concrete", 0f);
        }
    }
}
