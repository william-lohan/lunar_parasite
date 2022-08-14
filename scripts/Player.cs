using Godot;
using System;
public class Player : KinematicBody
{

    const string FORWARD = "FORWARD";
    const string BACKWARD = "BACKWARD";
    const string STRAFE_LEFT = "STRAFE_LEFT";
    const string STRAFE_RIGHT = "STRAFE_RIGHT";
    const string USE = "USE";

    Spatial head;
    Camera camera;

    float speed = 7f;
    float cam_accel = 40f;
    float mouse_sense = 0.1f;

    Vector3 snap;
    Vector3 direction = Vector3.Zero;
    Vector3 velocity = Vector3.Zero;
    Vector3 movement = Vector3.Zero;

    int accel = 7;

    public override void _Ready()
    {
        head = GetNode<Spatial>("Head");
        camera = head.GetChild<Camera>(0);
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseMotion mouseMotion) {
            RotateY(Mathf.Deg2Rad(-mouseMotion.Relative.x * mouse_sense));
            head.RotateX(Mathf.Deg2Rad(-mouseMotion.Relative.y * mouse_sense));
            Vector3 rotDeg = head.RotationDegrees;
            rotDeg.x = Mathf.Clamp(rotDeg.x, -89f, 89f);
            head.RotationDegrees = rotDeg;
        }
    }

    public override void _Process(float delta)
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


    public override void _PhysicsProcess(float delta)
    {
        direction = Vector3.Zero;
        var h_rot = GlobalTransform.basis.GetEuler().y;
	    var f_input = Input.GetActionStrength(BACKWARD) - Input.GetActionStrength(FORWARD);
	    var h_input = Input.GetActionStrength(STRAFE_RIGHT) - Input.GetActionStrength(STRAFE_LEFT);
	    direction = new Vector3(h_input, 0, f_input).Rotated(Vector3.Up, h_rot).Normalized();

        snap = -GetFloorNormal();

        velocity = velocity.LinearInterpolate(direction * speed, accel * delta);
	    movement = velocity;
	
	    MoveAndSlideWithSnap(movement, snap, Vector3.Up);

    }
}
