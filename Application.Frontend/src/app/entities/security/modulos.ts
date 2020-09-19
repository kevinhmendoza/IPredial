export class Permission {
    public hasRol: boolean;
    public Modulo: string;
    public Titulo: string;
    public Rol: string;
    public Url: string;
    public Prioridad: number;
    public Habilitado: boolean;
    public Descripcion: string;
    public Icono: string;
    public uiUrl: string;
    public Target: string;
};

export class Modulos {
    public Icono: string;
    public uiUrl: string;
    public Target: string;
    public Descripcion: string;
    public Titulo: string;
    public Modulo: string;
    public MenuId: string;
    public SubMenu: Permission[];
};
