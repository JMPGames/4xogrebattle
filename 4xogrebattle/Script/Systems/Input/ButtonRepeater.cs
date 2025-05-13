using Godot;

public class ButtonRepeater {
    private const int RATE = 50;
    private ulong _next;
    private string _button;

    public ButtonRepeater(string button) {
        _button = button;
    }

    public bool Update() {
        if (Input.IsActionJustPressed(_button)) {
            _next = Time.GetTicksMsec() + RATE;
            return true;
        }
        if (Input.IsActionPressed(_button)) {
            if (Time.GetTicksMsec() > _next) {
                _next = Time.GetTicksMsec() + RATE;
                return true;
            }
        }
        return false;
    }
}