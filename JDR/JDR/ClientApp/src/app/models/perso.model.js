"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Perso = /** @class */ (function () {
    function Perso(Id, IdUser, Nom, Prenom, Definition, Vivant, IdRace, NomRace, IdSousRace, NomSousRace, IdClasse, NomClasse, Lvl, Stats, posX, posY, Buff) {
        if (Stats === void 0) { Stats = []; }
        if (Buff === void 0) { Buff = []; }
        this.Id = Id;
        this.IdUser = IdUser;
        this.Nom = Nom;
        this.Prenom = Prenom;
        this.Definition = Definition;
        this.Vivant = Vivant;
        this.IdRace = IdRace;
        this.NomRace = NomRace;
        this.IdSousRace = IdSousRace;
        this.NomSousRace = NomSousRace;
        this.IdClasse = IdClasse;
        this.NomClasse = NomClasse;
        this.Lvl = Lvl;
        this.Stats = Stats;
        this.posX = posX;
        this.posY = posY;
        this.Buff = Buff;
    }
    return Perso;
}());
exports.Perso = Perso;
//# sourceMappingURL=perso.model.js.map