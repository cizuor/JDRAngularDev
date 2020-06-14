export class Perso {
  constructor(
    public Id: number,
    public IdUser: number,
    public Nom: string,
    public Prenom: string,
    public Definition: string,
    public Vivant: boolean,
    public IdRace: number,
    public NomRace: string,
    public IdSousRace: number,
    public NomSousRace: string,
    public IdClasse: number,
    public NomClasse: string,
    public Lvl: number,
    public Stats = [],
    public posX: number,
    public posY: number,
    public Buff = []
  ) { }

}
