"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Race = /** @class */ (function () {
    function Race(Id, Nom, Definition, Stat, StatDee) {
        if (Stat === void 0) { Stat = []; }
        if (StatDee === void 0) { StatDee = []; }
        this.Id = Id;
        this.Nom = Nom;
        this.Definition = Definition;
        this.Stat = Stat;
        this.StatDee = StatDee;
    }
    return Race;
}());
exports.Race = Race;
//# sourceMappingURL=race.model.js.map