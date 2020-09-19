import { ApisRestBase } from "./apis";

declare var jQuery: any;

export class UtilitiesAnibalService {
  public static DomainServer: string = ApisRestBase.UrlServer;

  public static AppName: string = "IPredial";

  public static Console(mensaje: any) {
    console.log(mensaje);
  }

  public static Loading: boolean = false;

  public static ShowLoading() {
    this.Loading = true;
  }

  public static HideLoading() {
    this.Loading = false;
  }

  public static SetUrlForIframe(idIframe: string, url: string) {
    jQuery("#" + idIframe).attr('src', url + "");
    setTimeout(() => {
      jQuery("#" + idIframe).attr('src', url + "");
    }, 500);
  }
}

