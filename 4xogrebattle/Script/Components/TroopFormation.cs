public class TroopFormation {
    private readonly Unit[] _units;

    public TroopFormation() {
        _units = new Unit[Common.MAX_UNITS_PER_TROOP];
    }

    public Unit[] GetAllUnits => _units;

    public Unit GetUnit(int i) {
        return Common.IndexAndValueExistsInArray(i, _units) ? _units[i] : null;
    }

    public bool SetUnit(int i, Unit unit) {
        if (!Common.IndexExistsInArray(i, _units)) {
            return false;
        }
        else if (_units[i] != null) {
            return false;
        }
        _units[i] = unit;
        return true;
    }

    public bool RemoveUnit(int i) {
        if (Common.IndexExistsInArray(i, _units)) {
            _units[i] = null;
            return true;
        }
        return false;
    }
}