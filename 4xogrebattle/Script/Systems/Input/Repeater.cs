using Godot;

public class Repeater {
    private const int RATE = 250;

    private ulong _next;
    private string _positiveAxis;
    private string _negativeAxis;

    public Repeater(string negativeAxis, string positionAxis) {
        _positiveAxis = positionAxis;
        _negativeAxis = negativeAxis;
    }

    public int Update() {
        int updatedValue = 0;
        int value = Mathf.RoundToInt(Input.GetAxis(_negativeAxis, _positiveAxis));
        if (value != 0) {
            if (Time.GetTicksMsec() > _next) {
                updatedValue = value;
                _next = Time.GetTicksMsec() + RATE;
            }
        }
        else {
            _next = 0;
        }
        return updatedValue;
    }
}