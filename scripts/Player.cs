using Godot;
using System;
public class Player : KinematicBody
{

    const string FORWARD = "FORWARD";
    const string BACKWARD = "BACKWARD";
    const string STRAFE_LEFT = "STRAFE_LEFT";
    const string STRAFE_RIGHT = "STRAFE_RIGHT";
    const string USE = "USE";

    [Export]
    bool isDay2;
    Spatial day2Target;

    Spatial head;
    Camera camera;
    private FootSteps sfx;

    float speed = 7f;
    float cam_accel = 40f;
    float mouse_sense = 0.1f;

    Vector3 snap;
    Vector3 direction = Vector3.Zero;
    Vector3 velocity = Vector3.Zero;
    Vector3 movement = Vector3.Zero;

    int accel = 7;

    public bool hasKey { get; set; }

    bool isDay2Event = false;
    SceneTreeTimer timer;

    public override void _Ready()
    {
        head = GetNode<Spatial>("Head");
        camera = head.GetChild<Camera>(0);
        sfx = GetNode<FootSteps>("SFX");
        Input.MouseMode = Input.MouseModeEnum.Captured;
        if (isDay2)
        {
            day2Target = GetNode<Spatial>("../monster");
        }
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (!isDay2Event)
        {
            if (inputEvent is InputEventMouseMotion mouseMotion) {
                RotateY(Mathf.Deg2Rad(-mouseMotion.Relative.x * mouse_sense));
                head.RotateX(Mathf.Deg2Rad(-mouseMotion.Relative.y * mouse_sense));
                Vector3 rotDeg = head.RotationDegrees;
                rotDeg.x = Mathf.Clamp(rotDeg.x, -89f, 89f);
                head.RotationDegrees = rotDeg;
            }
            if (inputEvent is InputEventKey keyEvent)
            {
                if (keyEvent.IsActionPressed("ui_cancel"))
                {
                    Input.MouseMode = Input.MouseModeEnum.Visible;
                }
            }
            if (inputEvent is InputEventMouseButton mouseEvent)
            {
                if (mouseEvent.IsPressed())
                {
                    Input.MouseMode = Input.MouseModeEnum.Captured;
                }
            }
        }
    }

    public override void _Process(float delta)
    {
        if (!isDay2Event)
        {
            if (Engine.GetFramesPerSecond() > Engine.IterationsPerSecond) {
                camera.SetAsToplevel(true);

                Vector3 Gtrans = head.GlobalTransform.origin;
                
            
            var cameraGT = camera.GlobalTransform;
                cameraGT.origin = camera.GlobalTransform.origin.LinearInterpolate(Gtrans, cam_accel * delta);
            camera.GlobalTransform = cameraGT;

                Vector3 camRot = camera.Rotation;
                camRot.y = Rotation.y;
                camRot.x = head.Rotation.x;
                camera.Rotation = camRot;
            } else {
                camera.SetAsToplevel(false);
                camera.GlobalTransform = head.GlobalTransform;
            }
        }

        sfx.checkStop();
    }


    public override void _PhysicsProcess(float delta)
    {
        if (!isDay2Event)
        {
            direction = Vector3.Zero;
            var h_rot = GlobalTransform.basis.GetEuler().y;
            var f_input = Input.GetActionStrength(BACKWARD) - Input.GetActionStrength(FORWARD);
            var h_input = Input.GetActionStrength(STRAFE_RIGHT) - Input.GetActionStrength(STRAFE_LEFT);
            direction = new Vector3(h_input, 0, f_input).Rotated(Vector3.Up, h_rot).Normalized();
            if (direction != Vector3.Zero)
            {
                sfx.playRandom();
            }
            snap = -GetFloorNormal();

            velocity = velocity.LinearInterpolate(direction * speed, accel * delta);
            movement = velocity;
        
            MoveAndSlideWithSnap(movement, snap, Vector3.Up);
        }
        else
        {
            camera.LookAt(day2Target.Translation, Vector3.Up);
        }
        sfx.checkStop();
    }

    public void StartDay2Event(Node body)
    {
        if (body is Player && !isDay2Event && isDay2)
        {
            isDay2Event = true;
            timer = GetTree().CreateTimer(14.0f);
            timer.Connect("timeout", this, nameof(FinishEvent));
        }
    }

    public void FinishEvent()
    {
        isDay2Event = false;
        isDay2 = false;
    }

}
