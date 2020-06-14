"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Stat = /** @class */ (function () {
    function Stat(Id, Nom, Definition, Type, Stats, StatUtils) {
        if (StatUtils === void 0) { StatUtils = []; }
        this.Id = Id;
        this.Nom = Nom;
        this.Definition = Definition;
        this.Type = Type;
        this.Stats = Stats;
        this.StatUtils = StatUtils;
    }
    return Stat;
}());
exports.Stat = Stat;
//# sourceMappingURL=stat.model.js.map