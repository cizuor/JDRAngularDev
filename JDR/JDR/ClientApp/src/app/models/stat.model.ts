export class Stat {
  constructor(
    public Id: number,
    public Nom: string,
    public Definition: string,
    public Type: number,
    public Stats: number,
    public StatUtils =[]
  ) { }

}
