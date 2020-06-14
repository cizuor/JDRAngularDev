export class Race {
  constructor(
    public Id: number,
    public Nom: string,
    public Definition: string,
    public Stat = [],
    public StatDee =[]
  ) { }

}
