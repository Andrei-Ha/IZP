function GetRadUpload(K){return window[K]; } ; if (typeof(RadUploadNameSpace)=="\x75ndefined")RadUploadNameSpace= {} ; RadUploadNameSpace.RadUpload= function (J){ this.i9= false; RadControlsNamespace.EventMixin.Initialize(this ); RadControlsNamespace.DomEventsMixin.Initialize(this ); this.Id=J[0]; this.I9(document.getElementById(J[1])); this.oa=J[2]; this.Oa=J[3]; this.Enabled=J[4]; this.la=J[5]; this.ia=J[6]; this.EnableFileInputSkinning=J[7]; if (RadControlsNamespace.Browser.IsSafari || (RadControlsNamespace.Browser.IsOpera && !RadControlsNamespace.Browser.IsOpera9)){ this.EnableFileInputSkinning= false; } this.ReadOnlyFileInputs=J[8]; this.AllowedFileExtensions=J[9]; this.Ia=J[10]&1; this.ob=J[10]&2; this.Ob=J[10]&4; this.OnClientAdded=J[11]; this.OnClientAdding=J[12]; this.OnClientDeleting=J[13]; this.OnClientClearing=J[14]; this.OnClientFileSelected=J[15]; this.OnClientDeletingSelected=J[16]; this.CurrentIndex=0; this.lb=document.getElementById(this.Id+"B\x75ttonArea"); this.ListContainer=document.getElementById(this.Id+"ListContaine\x72"); if (!document.readyState || document.readyState=="\x63omplete"){ this.InnerConstructor(); }else { this.AttachDomEvent(window,"\x6coad","InnerConstr\x75\x63tor"); }} ; RadUploadNameSpace.RadUpload.prototype= {InnerConstructor:function (N){ this.ib(); this.AddButton=this.InitButton(document.getElementById(this.Id+"AddButton"),"Add","AddFil\x65Input"); this.DeleteButton=this.InitButton(document.getElementById(this.Id+"\104\x65leteBut\x74\x6fn"),"\x44elete","D\x65\x6ceteSele\x63\x74edF\x69\x6ceI\x6eputs"); this.Ib=this.oc(); var Oc=this.la==0?this.ia:Math.min(this.ia,this.la); for (var i=0; i<Oc; i++){ this.AddFileInput(); } this.lc(); this.i9= true; } ,AddFileInput:function (N){var l3=this.AddFileInputAt(this.ListContainer.rows.length); if (this.i9){try {l3.focus(); }catch (ex){}}} ,AddFileInputAt:function (index){if (typeof(index)=="\165\x6e\x64efined" || index>this.ListContainer.rows.length){index=this.ListContainer.rows.length; }if (this.la>0 && index>=this.la)return; if (this.i9){var ic=this.RaiseEvent("O\x6e\x43lient\x41\x64din\x67",new RadUploadNameSpace.RadUploadEventArgs(null)); if (ic== false){return; }} this.Ic(index); } ,Ic:function (index){var od=this.ListContainer.insertRow(index); this.AttachDomEvent(od,"c\x6c\x69ck","Row\x43\x6cicked"); var Od; if (this.Ia){Od=od.insertCell(od.cells.length); this.ld(Od); }Od=od.insertCell(od.cells.length); this.oe(Od); if (this.Ob){Od=od.insertCell(od.cells.length); this.Oe(Od); }if (this.ob){Od=od.insertCell(od.cells.length); this.le(Od); } this.lc(); this.RaiseEvent("\x4fnCli\x65\x6etAdd\x65\x64", {Row:od } ); this.CurrentIndex++; return od; } ,ld:function (container){var ie=document.createElement("input"); ie.type="\x63heckbox"; ie.id=this.Id+"checkbox"+this.CurrentIndex; container.appendChild(ie); ie.className="RadUploadFi\x6c\x65Sel\x65\x63tor"; ie.disabled=!this.Enabled; return ie; } ,Oe:function (container){var button=document.createElement("input"); button.type="butt\x6f\x6e"; button.id=this.Id+"c\x6c\x65ar"+this.CurrentIndex; container.appendChild(button); this.InitButton(button,"Clear"); button.className="\x52adUpload\x43\x6cearB\x75\x74to\x6e"; button.name="\x43learInput"; button.disabled=!this.Enabled; return button; } ,le:function (container){var button=document.createElement("\x69nput"); button.type="\x62\x75tton"; button.id=this.Id+"remov\x65"+this.CurrentIndex; container.appendChild(button); button.value=RadUploadNameSpace.Localization[this.oa]["Rem\x6f\x76e"]; button.className="\x52adUploa\x64\x52emove\x42\x75tt\x6f\x6e"; button.name="\x52\x65moveR\x6f\x77"; button.disabled=!this.Enabled; return button; } ,oe:function (container){var l3=this.Ie(); this.AttachDomEvent(l3,"\x63hange","\x46ileS\x65\x6cected"); if (this.EnableFileInputSkinning){l3.className="Rea\x6c\x46ileInp\x75\x74"; var div=document.createElement("d\x69\x76"); container.appendChild(div); div.style.position="rel\x61\x74ive"; div.appendChild(this.Ib.cloneNode( true)); div.appendChild(l3); if (!this.ReadOnlyFileInputs){ this.AttachDomEvent(l3,"\x6beyup","\x53\x79ncFileI\x6e\x70utCo\x6e\164\x65nt"); }else { this.AttachDomEvent(l3,"keydown","CancelEvent"); }return div; }else {container.appendChild(l3); l3.className="\x4eoSkinne\x64\x46ileU\x6e\x70ut"; if (this.ReadOnlyFileInputs){ this.AttachDomEvent(l3,"keydown","\103a\x6e\x63elEven\x74"); }return l3; }} ,CancelEvent:function (N){if (!N)N=window.event; if (!N)return false; N.returnValue= false; N.cancelBubble= true; if (N.stopPropagation){N.stopPropagation(); }if (N.preventDefault){N.preventDefault(); }return false; } ,ClearFileInputAt:function (index){var od=this.ListContainer.rows[index]; if (od){var ic=this.RaiseEvent("\x4fnClientC\x6c\145a\x72\x69ng",new RadUploadNameSpace.RadUploadEventArgs(this.GetFileInputFrom(od))); if (ic== false){return false; } this.DeleteFileInputAt(index, true); this.Ic(index, true); }} ,oc:function (){var of=document.createElement("div"); of.style.position="\x61bsol\x75\x74e"; of.style.top=0; of.style.left=0; of.style.zIndex=1; var Of=document.createElement("input"); Of.type="\x74ext"; Of.className="\x52adUplo\x61\x64Inpu\x74\x46iel\x64"; of.appendChild(Of); var If=document.createElement("\x69nput"); If.type="\x62utton"; of.appendChild(If); this.InitButton(If,"\123e\x6c\x65ct"); If.disabled=!this.Enabled; If.className="\x52adUploadSe\x6c\x65ctB\x75\x74ton"; return of; } ,Ie:function (){var l3=document.createElement("\x69nput"); l3.type="\x66\x69le"; l3.name=this.GetID("\x66ile"); l3.id=this.GetID("f\x69\x6ce"); l3.disabled=!this.Enabled; return l3; } ,DeleteFileInputAt:function (index,og){var od=this.ListContainer.rows[index]; if (od){if (!og){var ic=this.RaiseEvent("OnCl\x69\x65ntDel\x65\x74ing",new RadUploadNameSpace.RadUploadEventArgs(this.GetFileInputFrom(od))); if (ic== false){return false; }}od.parentNode.removeChild(od); this.lc(); }} ,DeleteSelectedFileInputs:function (N){var Og=[]; var lg=[]; for (var i=this.ListContainer.rows.length-1; i>=0; i--){var Ig=this.ListContainer.rows[i]; var oh=Ig.cells[0].childNodes[0]; if (oh.checked){lg[lg.length]=i; Og[Og.length]=this.GetFileInputFrom(Ig); }}var ic=this.RaiseEvent("OnC\x6cientDelet\x69\x6egSe\x6c\x65ct\x65\x64",new RadUploadNameSpace.RadUploadDeleteSelectedEventArgs(Og)); if (ic== false){return; }for (var i=0; i<lg.length; i++){ this.DeleteFileInputAt(lg[i], true); }} ,ib:function (){var Oh=this.ListContainer.rows[0]; Oh.parentNode.removeChild(Oh); } ,FileSelected:function (e){if (this.EnableFileInputSkinning){ this.SyncFileInputContent(e); }var l3=e.srcElement?e.srcElement:e.target; l3.alt=l3.title=l3.value; this.RaiseEvent("\117\x6eClientFil\x65\x53ele\x63ted",new RadUploadNameSpace.RadUploadEventArgs(l3)); } ,GetFileInputFrom:function (od){var lh=od.getElementsByTagName("input"); for (var i=0; i<lh.length; i++){if (lh[i].type=="\x66ile"){return lh[i]; }}return null; } ,GetFileInputs:function (){var O3=[]; for (var i=0; i<this.ListContainer.rows.length; i++){O3[O3.length]=this.GetFileInputFrom(this.ListContainer.rows[i]); }return O3; } ,GetID:function (F){return this.Id+F+this.CurrentIndex; } ,ih:function (l){if (l){if (l.tagName.toLowerCase()=="\x74r"){return l; }else {return this.ih(l.parentNode); }}return null; } ,InitButton:function (button,Ih,oi){if (button){button.value=RadUploadNameSpace.Localization[this.oa][Ih]; if (this.Enabled){if (oi)this.AttachDomEvent(button,"click",oi); }else {button.disabled= true; }}return button; } ,IsExtensionValid:function (Oi){if (Oi=="")return true; for (var i=0; i<this.AllowedFileExtensions.length; i++){var ii=this.AllowedFileExtensions[i].substring(2); var Ii=new RegExp("\x2e"+ii+"\x24","ig"); if (Oi.match(Ii)){return true; }}return false; } ,RowClicked:function (e){var srcElement=e.srcElement?e.srcElement:e.target; var oj=this.ih(srcElement); if (srcElement.name=="\x52emoveRow"){ this.DeleteFileInputAt(oj.rowIndex); }else if (srcElement.name=="\x43learIn\x70\x75t"){ this.ClearFileInputAt(oj.rowIndex); }} ,lc:function (){ this.Oj(this.DeleteButton,this.ListContainer.rows.length>0); this.Oj(this.AddButton,(this.la<=0) || (this.ListContainer.rows.length<this.la)); } ,Oj:function (button,lj){if (button){button.className=lj?"RadU\x70\x6coadBut\x74\x6fn": "R\x61\x64UploadB\x75\x74ton\x44\x69sa\x62\154\x65\144"; }} ,SyncFileInputContent:function (e){var l3=e.srcElement?e.srcElement:e.target; var ij=l3.parentNode.childNodes[0].childNodes[0]; if (l3 !== ij){ij.value=l3.value; ij.title=l3.value; ij.disabled= true; }} ,I9:function (form){if (!form)form=document.forms[0]; form.enctype=form.encoding="\x6dultipart/fo\x72\x6d-dat\x61"; } ,ValidateExtensions:function (){for (var i=0; i<this.ListContainer.rows.length; i++){var Ij=this.GetFileInputFrom(this.ListContainer.rows[i]).value; if (!this.IsExtensionValid(Ij)){return false; }}return true; }} ; RadUploadNameSpace.RadUpload.ok= function (l3){} ;;if (typeof window.RadControlsNamespace=="\165ndef\x69\x6eed"){window.RadControlsNamespace= {} ; }if (typeof(window.RadControlsNamespace.Browser)=="undefined" || typeof(window.RadControlsNamespace.Browser.Version)==null || window.RadControlsNamespace.Browser.Version<1){window.RadControlsNamespace.Browser= {Version: 1 } ; window.RadControlsNamespace.Browser.ParseBrowserInfo= function (){ this.IsMacIE=(navigator.appName=="Micro\x73\x6fft I\x6e\x74ern\x65t Explo\x72\145r") && ((navigator.userAgent.toLowerCase().indexOf("mac")!=-1) || (navigator.appVersion.toLowerCase().indexOf("\x6dac")!=-1)); this.IsSafari=(navigator.userAgent.toLowerCase().indexOf("saf\x61\x72i")!=-1); this.IsMozilla=window.netscape && !window.opera; this.IsNetscape=/\x4e\x65\x74\x73\x63\x61\x70\x65/.test(navigator.userAgent); this.IsOpera=window.opera; this.IsOpera9=window.opera && (parseInt(window.opera.version())>8); this.IsIE=!this.IsMacIE && !this.IsMozilla && !this.IsOpera && !this.IsSafari; this.IsIE7=/\x4d\x53\x49\x45\x20\x37/.test(navigator.appVersion); this.StandardsMode=this.IsSafari || this.IsOpera9 || this.IsMozilla || document.compatMode=="\x43SS1C\x6f\x6dpat"; this.IsMac=/\x4d\x61\x63/.test(navigator.userAgent); };RadControlsNamespace.Browser.ParseBrowserInfo(); };if (typeof window.RadControlsNamespace=="u\x6e\x64efined"){window.RadControlsNamespace= {} ; }RadControlsNamespace.DomEventsMixin= function (){} ; RadControlsNamespace.DomEventsMixin.Initialize= function (O){O.AttachDomEvent=this.AttachDomEvent; O.DetachDomEvent=this.DetachDomEvent; O.DisposeDomEvents=this.DisposeDomEvents; O.ClearEventPointers=this.ClearEventPointers; O.RegisterForAutomaticDisposal=this.RegisterForAutomaticDisposal; O.o=this.o; O.CreateEventHandler=this.CreateEventHandler; O.I=this.I; O.ClearEventPointers(); } ; RadControlsNamespace.DomEventsMixin.CreateEventHandler= function (A){var U=this ; return function (e){if (!e)e=window.event; return U[A](e); };} ; RadControlsNamespace.DomEventsMixin.AttachDomEvent= function (Z,z,W){var w=this.CreateEventHandler(W); this.V[this.V.length]=[Z,z,w]; this.I(Z,z,w); } ; RadControlsNamespace.DomEventsMixin.I= function (Z,z,w){if (Z.attachEvent){Z.attachEvent("on"+z,w); }else if (Z.addEventListener){Z.addEventListener(z,w, false); }} ; RadControlsNamespace.DomEventsMixin.DetachDomEvent= function (Z,z,w){if (Z.detachEvent){Z.detachEvent("o\x6e"+z,w); }} ; RadControlsNamespace.DomEventsMixin.DisposeDomEvents= function (){for (var i=0; i<this.V.length; i++){ this.DetachDomEvent(this.V[i][0],this.V[i][1],this.V[i][2]); } this.ClearEventPointers(); } ; RadControlsNamespace.DomEventsMixin.RegisterForAutomaticDisposal= function (v){var T=this ; var t=this.CreateEventHandler(v); var S= function (){t(); T.DisposeDomEvents(); T=null; } ; this.I(window,"\165\x6eload",S); } ; RadControlsNamespace.DomEventsMixin.ClearEventPointers= function (){ this.V=[]; } ;;if (typeof window.RadControlsNamespace=="u\x6e\x64\x65fine\x64"){window.RadControlsNamespace= {} ; }if (typeof(window.RadControlsNamespace.EventMixin)=="u\x6e\x64efined" || typeof(window.RadControlsNamespace.EventMixin.Version)==null || window.RadControlsNamespace.EventMixin.Version<2){RadControlsNamespace.EventMixin= {Version: 2,Initialize:function (O){O._listeners= {} ; O._eventsEnabled= true; O.AttachEvent=this.AttachEvent; O.DetachEvent=this.DetachEvent; O.RaiseEvent=this.RaiseEvent; O.EnableEvents=this.EnableEvents; O.DisableEvents=this.DisableEvents; O.DisposeEventHandlers=this.DisposeEventHandlers; } ,DisableEvents:function (){ this._eventsEnabled= false; } ,EnableEvents:function (){ this._eventsEnabled= true; } ,AttachEvent:function (z,R){if (!this._listeners[z]){ this._listeners[z]=[]; } this._listeners[z][this._listeners[z].length]=(RadControlsNamespace.EventMixin.ResolveFunction(R)); } ,DetachEvent:function (z,R){var r=this._listeners[z]; if (!r){return false; }var Q=RadControlsNamespace.EventMixin.ResolveFunction(R); for (var i=0; i<r.length; i++){if (Q==r[i]){r.splice(i,1); return true; }}return false; } ,DisposeEventHandlers:function (){for (var z in this._listeners){var r=null; if (this._listeners.hasOwnProperty(z)){r=this._listeners[z]; for (var i=0; i<r.length; i++){r[i]=null; }r=null; }}} ,ResolveFunction:function (P){if (typeof(P)=="\146unction"){return P; }else if (typeof(window[P])=="\x66u\x6e\x63tion"){return window[P]; }else {return new Function("var Sen\x64\x65r = \x61\x72gum\x65\156\x74\x73[0]\x3b\x20va\x72 Ar\x67\x75me\x6e\164s\x20\075\x20argu\x6d\145n\x74s[1]\x3b"+P); }} ,RaiseEvent:function (z,N){if (!this._eventsEnabled){return true; }var n= true; if (this[z]){var M=RadControlsNamespace.EventMixin.ResolveFunction(this[z])(this,N); if (typeof(M)=="\x75ndef\x69\x6eed"){M= true; }n=n && M; }if (!this._listeners[z])return n; for (var i=0; i<this._listeners[z].length; i++){var R=this._listeners[z][i]; var M=R(this,N); if (typeof(M)=="\x75\156def\x69\x6eed"){M= true; }n=n && M; }return n; }};};if (typeof(RadUploadNameSpace)=="\x75\x6edefined")RadUploadNameSpace= {} ; if (typeof(RadUploadNameSpace.Localization)=="\x75ndefined")RadUploadNameSpace.Localization=[]; RadUploadNameSpace.Localization.ProcessRawArray= function (m){var L=m[0]; if (typeof(RadUploadNameSpace.Localization[L])=="\x75ndefi\x6e\x65d"){RadUploadNameSpace.Localization[L]=[]; }for (var i=1; i<m.length; i+=2){RadUploadNameSpace.Localization[L][m[i]]=m[i+1]; }} ;;if (typeof window.RadControlsNamespace=="undefine\x64"){window.RadControlsNamespace= {} ; }if (typeof(window.RadControlsNamespace.Overlay)=="\x75ndefined" || typeof(window.RadControlsNamespace.Overlay.Version)==null || window.RadControlsNamespace.Overlay.Version<.11e1){window.RadControlsNamespace.Overlay= function (l){if (!this.SupportsOverlay()){return; } this.Element=l; this.Shim=document.createElement("IFRAME"); this.Shim.src="javascript:\x27\047\x3b"; this.Element.parentNode.insertBefore(this.Shim,this.Element); if (l.style.zIndex>0){ this.Shim.style.zIndex=l.style.zIndex-1; } this.Shim.style.position="\141b\x73\x6flute"; this.Shim.style.border="\x30\160x"; this.Shim.frameBorder=0; this.Shim.style.filter="pro\x67\x69d:DXIm\x61\x67eTr\x61nsform\x2e\x4dic\x72\x6fsof\x74.Al\x70ha(st\x79\154e\x3d\060\x2copaci\x74y=0)"; this.Shim.disabled="disabled"; };window.RadControlsNamespace.Overlay.Version=.11e1; RadControlsNamespace.Overlay.prototype.SupportsOverlay= function (){return RadControlsNamespace.Browser.IsIE; };RadControlsNamespace.Overlay.prototype.Update= function (){if (!this.SupportsOverlay()){return; } this.Shim.style.top=this.ToUnit(this.Element.style.top); this.Shim.style.left=this.ToUnit(this.Element.style.left); this.Shim.style.width=this.Element.offsetWidth+"px"; this.Shim.style.height=this.Element.offsetHeight+"px"; };RadControlsNamespace.Overlay.prototype.ToUnit= function (value){if (!value)return "\x30px"; return parseInt(value)+"\x70x"; };RadControlsNamespace.Overlay.prototype.Dispose= function (){if (!this.SupportsOverlay()){return; }if (this.Shim.parentNode){ this.Shim.parentNode.removeChild(this.Shim); } this.Element=null; this.Shim=null; };};function GetRadProgressArea(K){return window[K]; } ; if (typeof(RadUploadNameSpace)=="u\x6e\x64efined")RadUploadNameSpace= {} ; RadUploadNameSpace.k="\x50anel"; RadUploadNameSpace.RadProgressArea= function (J){ this.Id=J[0]; this.OnClientProgressUpdating=J[1]; this.OnClientProgressBarUpdating=J[2]; this.H=J[3]; if (!this.H){alert("\x43ould n\x6f\x74 fin\x64\x20an\x20instanc\x65\x20of \x52\141\x64Pro\x67ressMa\x6eager o\x6e the \x70\141g\x65. Ar\x65\040\x79ou\x20mis\x73ing \x74he c\x6fntro\x6c dec\x6cara\x74ion\x3f"); }RadControlsNamespace.EventMixin.Initialize(this ); RadControlsNamespace.DomEventsMixin.Initialize(this ); this.Element=document.getElementById(this.Id); this.PrimaryProgressBarElement=this.FindElement("\120ri\x6d\x61ryPro\x67\x72ess\x42\141r"); this.PrimaryTotalElement=this.FindElement("PrimaryTot\x61\x6c"); this.PrimaryValueElement=this.FindElement("\x50rimaryVal\x75\x65"); this.PrimaryPercentElement=this.FindElement("\x50\x72imaryP\x65\x72cent"); this.SecondaryProgressBarElement=this.FindElement("\x53\x65conda\x72\x79Prog\x72\x65ss\x42\x61r"); this.SecondaryTotalElement=this.FindElement("\x53\x65conda\x72\x79Tota\x6c"); this.SecondaryValueElement=this.FindElement("Secondary\x56\x61lue"); this.SecondaryPercentElement=this.FindElement("\x53econd\x61\x72yPerc\x65\x6et"); this.h=this.FindElement("CurrentOper\x61\x74ion"); this.TimeElapsedElement=this.FindElement("TimeEla\x70\x73ed"); this.TimeEstimatedElement=this.FindElement("Ti\x6d\x65Estimat\x65\x64"); this.SpeedElement=this.FindElement("\x53peed"); this.CancelButtonElement=this.FindElement("\x43ancelB\x75\x74ton"); this.CancelClicked= false; if (this.CancelButtonElement){ this.AttachDomEvent(this.CancelButtonElement,"\x63\x6cick","\x43\141nce\x6c\x52eques\x74"); }if (typeof(RadUploadNameSpace.ProgressAreas)=="\x75\156def\x69\x6eed"){RadUploadNameSpace.ProgressAreas=[]; } this.RegisterForAutomaticDisposal("\x48ide"); RadUploadNameSpace.ProgressAreas[RadUploadNameSpace.ProgressAreas.length]=this ; } ; RadUploadNameSpace.RadProgressArea.prototype= {Update:function (G){if (this.RaiseEvent("\x4fnClien\x74\x50rogr\x65\x73sUp\x64\x61tin\x67", {ProgressData:G } )== false)return; this.Show(); if (this.RaiseEvent("\x4fnClientPr\x6f\x67ress\x42\x61rU\x70\x64ati\x6e\x67", {ProgressValue:G.PrimaryPercent,ProgressBarElementName: "\x50rimaryPro\x67\x72essB\x61\x72",ProgressBarElement: this.PrimaryProgressBarElement } )!= false){ this.UpdateHorizontalProgressBar(this.PrimaryProgressBarElement,G.PrimaryPercent); }if (this.RaiseEvent("OnC\x6c\x69entPro\x67\x72ess\x42\141r\x55\x70dat\x69\x6eg", {ProgressValue:G.SecondaryPercent,ProgressBarElementName: "Second\x61\x72yProg\x72\x65ssBa\x72",ProgressBarElement: this.SecondaryProgressBarElement } )!= false){ this.UpdateHorizontalProgressBar(this.SecondaryProgressBarElement,G.SecondaryPercent); } this.UpdateTextIndicator(this.PrimaryTotalElement,G.PrimaryTotal); this.UpdateTextIndicator(this.PrimaryValueElement,G.PrimaryValue); this.UpdateTextIndicator(this.PrimaryPercentElement,G.PrimaryPercent); this.UpdateTextIndicator(this.SecondaryTotalElement,G.SecondaryTotal); this.UpdateTextIndicator(this.SecondaryValueElement,G.SecondaryValue); this.UpdateTextIndicator(this.SecondaryPercentElement,G.SecondaryPercent); this.UpdateTextIndicator(this.h,G.CurrentOperationText); this.UpdateTextIndicator(this.TimeElapsedElement,G.TimeElapsed); this.UpdateTextIndicator(this.TimeEstimatedElement,G.TimeEstimated); this.UpdateTextIndicator(this.SpeedElement,G.Speed); } ,Show:function (){ this.Element.style.display=""; if (this.Element.style.position=="\x61\x62\x73olute"){if (typeof(this.Overlay)=="undef\x69\x6eed"){ this.Overlay=new RadControlsNamespace.Overlay(this.Element); } this.Overlay.Update(); }} ,Hide:function (){ this.Element.style.display="\x6eone"; if (this.Overlay){ this.Overlay.Dispose(); this.Overlay=null; }} ,UpdateHorizontalProgressBar:function (l,g){if (l && typeof(g)!="\x75ndefined")l.style.width=g+"%"; } ,UpdateVerticalProgressBar:function (l,g){if (l && typeof(g)!="\x75\x6edefined")l.style.height=g+"%"; } ,UpdateTextIndicator:function (l,text){if (l && typeof(text)!="und\x65\x66ined"){if (typeof(l.value)=="string")l.value=text; else if (typeof(l.innerHTML)=="string")l.innerHTML=text; }} ,CancelRequest:function (){ this.CancelClicked= true; } ,FindElement:function (F){var f=this.Id+"_"+RadUploadNameSpace.k+"_"+F; return document.getElementById(f); }};;function GetRadProgressManager(){return window["R\x61\x64ProgressM\x61\x6eage\x72"]; } ; if (typeof(RadUploadNameSpace)=="undefine\x64")RadUploadNameSpace= {} ; RadUploadNameSpace.RadProgressManager= function (J){RadControlsNamespace.EventMixin.Initialize(this ); RadControlsNamespace.DomEventsMixin.Initialize(this ); this.D=Math.max(J[0],50); var d=J[1]; this.EnableMemoryOptimizationIdentifier=J[2]; this.UniqueRequestIdentifier=J[3]; this.C=J[4]; this.OnClientProgressStarted=J[5]; this.OnClientProgressUpdating=J[6]; this.FormId=J[7]; this.c=J[8]; this.EnableMemoryOptimization=J[9]; this.SuppressMissingHttpModuleError=J[10]; this.OnClientSubmitting=J[11]; this.TimeFormat="%HOURS\x25\072\x25MINUTES\x25\x3a%S\x45\x43ON\x44\x53%s"; var form=document.getElementById(this.FormId); if (!form){form=document.forms[0]; } this.B(form); if (this.c== true){ this.RegisterForSubmit(form); } this.o0=this.O0(d); this.l0= false; if (typeof(RadUploadNameSpace.ProgressAreas)=="\165\x6e\x64efined"){RadUploadNameSpace.ProgressAreas=[]; }} ; RadUploadNameSpace.RadProgressManager.prototype= {ClientSubmitHandler:function (N){if (this.RaiseEvent("OnClien\x74Submitti\x6e\x67")== false){ this.CancelEvent(N); return false; } this.StartProgressPolling(); } ,StartProgressPolling:function (){ this.InitSelectedFilesCount(); this.RaiseEvent("\x4fnClientProgr\x65\x73sSt\x61\162\x74\x65d"); this.i0=new Date(); this.MakeCallback(); } ,MakeCallback:function (){if (!this.l0){ this.l0= true; this.I0(); }} ,HandleCallback:function (){if (this.o1.readyState!=4)return; this.l0= false; if (this.ErrorOccured())return; var responseText=this.o1.responseText; if (responseText){try {eval(responseText); }catch (ex){ this.O1(); return; }if (rawProgressData){if (this.EnableMemoryOptimization== true && !this.SuppressMissingHttpModuleError && rawProgressData.ProgressError){alert(rawProgressData.ProgressError); return; }if (rawProgressData.InProgress){if (this.l1>0 || rawProgressData.RadProgressContextCustomCounters){ this.ModifyProgressData(rawProgressData); if (!this.UpdateProgressAreas(rawProgressData)){window.location.href=window.location.href; return; }}}}}window.setTimeout(this.CreateEventHandler("\x4d\x61\x6beCallb\x61\x63k"),this.D); } ,ErrorOccured:function (){if (!document.all)return false; if (this.o1.status==404){ this.i1(); }else if (this.o1.status>0 && this.o1.status!=200){ this.I1(); }else return false; return true; } ,i1:function (){alert("\x52adUpload \x41\x6aax \x63\x61l\x6c\x62ack\x20\x65rr\x6f\x72. \x53our\x63\145\x20\165r\x6c was \x6e\157t\x20found\x3a \012\x0d\x0a\015"+this.o0+"\x0a\x0d\012\x0dDid you\x20\x72eg\x69\163t\x65\x72 th\x65\040R\x61dUplo\x61\144P\x72ogres\x73\110a\x6edler\x20\151\x6e\040\x77eb.c\x6fnfig\x3f"+"\015\x0a\015\012\x50leas\x65\x2c s\x65\x65 t\x68\x65 he\x6c\160 \x66or mo\x72e det\x61\151l\x73: Rad\x55pload\x202.x \x2d\040\x55sing\x20RadU\x70loa\x64 - C\x6fnfi\x67urat\x69on \x2d Ra\x64Upl\x6fadP\x72og\x72ess\x48and\x6cer\x2e"); } ,I1:function (){alert("RadUpload A\x6a\x61x c\x61\x6clba\x63\153 \x65\162ro\x72\056 \x53ource\x20\165r\x6c\040r\x65turne\x64\040e\x72ror:\x20"+this.o1.status+" \012\x0d\012\015"+this.o1.o2+"\x20\012\x0d\x0a\015"+this.o0+"\012\x0d\x0a\015\x44\151d\x20\x79ou\x20\x72egi\x73\164e\x72\040t\x68e Rad\x55\160l\x6fadPro\x67ressH\x61ndle\x72\040\x69n we\x62.con\x66ig?"+"\015\012\x0d\012\x50\154e\x61\x73e, \x73\145e\x20\x74he\x20\x68el\x70 for \x6d\157r\x65 deta\x69\154\x73\072 \x52adUp\x6coad \x32.x -\x20Usi\x6e\147\x20Rad\x55ploa\x64 - \x43onf\x69gur\x61tio\x6e - \x52adU\x70lo\x61dPr\x6fgr\x65ssH\x61nd\x6cer\x2e"); } ,O1:function (){alert("RadUpl\x6f\x61d Aj\x61\x78 cal\x6cback er\x72\x6fr. \x53\157u\x72ce url\x20return\x65d inv\x61\154i\x64 cont\x65nt: \x0a\015\x0a\015"+this.o1.responseText+"\012\x0d\x0a\015"+this.o0+"\012\x0d\x0a\015\x44id you \x72\145g\x69\x73te\x72\x20th\x65\x20R\x61\x64Up\x6c\157a\x64Progr\x65ssHan\x64ler i\x6e web\x2e\143\x6fnfig\x3f"+"\015\x0a\x0d\012\x50\154ea\x73\x65, \x73\x65e \x74\x68e h\x65\154\x70\040f\x6f\162 \x6dore d\x65tails\x3a\040\x52\141\x64\125\x70load\x202.x \x2d Usi\x6eg R\x61dUpl\x6fad \x2d Co\x6efigu\x72ati\x6fn \x2d Ra\x64Upl\x6fad\x50rog\x72es\x73Han\x64le\x72."); } ,UpdateProgressAreas:function (rawProgressData){ this.RaiseEvent("\x4f\x6eClient\x50\x72ogre\x73\x73Up\x64\x61tin\x67", {ProgressData:rawProgressData } ); for (var i=0; i<RadUploadNameSpace.ProgressAreas.length; i++){var O2=RadUploadNameSpace.ProgressAreas[i]; if (O2.CancelClicked){return false; }O2.Update(rawProgressData); }return true; } ,ModifyProgressData:function (rawProgressData){var l2=new Date()-this.i0; if (typeof(rawProgressData.TimeElapsed)=="undefined")rawProgressData.TimeElapsed=this.GetFormattedTime(this.ToSeconds(l2)); if (rawProgressData.RadUpload){var i2=rawProgressData.RadUpload.RequestSize; var I2=rawProgressData.RadUpload.Bytes; if (typeof(rawProgressData.PrimaryTotal)=="\x75ndefined")rawProgressData.PrimaryTotal=this.FormatBytes(i2); if (typeof(rawProgressData.PrimaryValue)=="\x75ndefin\x65\x64")rawProgressData.PrimaryValue=this.FormatBytes(I2); if (typeof(rawProgressData.PrimaryPercent)=="undefined")rawProgressData.PrimaryPercent=Math.round(100*I2/i2); if (typeof(rawProgressData.SecondaryTotal)=="\x75\x6edefined")rawProgressData.SecondaryTotal=this.l1; if (typeof(rawProgressData.SecondaryValue)=="u\x6e\x64efined")rawProgressData.SecondaryValue=rawProgressData.RadUpload.FilesCount; if (typeof(rawProgressData.SecondaryPercent)=="\x75ndefined")rawProgressData.SecondaryPercent=Math.round(100*rawProgressData.RadUpload.FilesCount/(this.l1!=0?this.l1: 1)); if (typeof(rawProgressData.CurrentOperationText)=="\x75ndefined")rawProgressData.CurrentOperationText=rawProgressData.RadUpload.CurrentFileName; if (typeof(rawProgressData.Speed)=="undefi\x6e\x65d"){if (this.ToSeconds(l2)==0){rawProgressData.Speed=this.FormatBytes(0)+"\x2fs"; }else {rawProgressData.Speed=this.FormatBytes(rawProgressData.RadUpload.Bytes/this.ToSeconds(l2))+"/s"; }}}if (typeof(rawProgressData.TimeEstimated)=="\x75\x6edefine\x64" && typeof(rawProgressData.PrimaryPercent)=="\x6eumber"){if (rawProgressData.PrimaryPercent==0){rawProgressData.TimeEstimated=this.GetFormattedTime(this.ToSeconds(359999000)); }else {rawProgressData.TimeEstimated=this.GetFormattedTime(this.ToSeconds(l2*(100/rawProgressData.PrimaryPercent-1))); }}} ,ToSeconds:function (o3){return Math.round(o3/1000); } ,InitSelectedFilesCount:function (){ this.l1=0; var O3=document.getElementsByTagName("\x69nput"); for (var i=0; i<O3.length; i++){var l3=O3[i]; if (l3.type=="\x66\x69le" && l3.value!=""){ this.l1++; }}} ,CancelEvent:function (N){if (!N)N=window.event; if (!N)return false; N.returnValue= false; N.cancelBubble= true; if (N.stopPropagation){N.stopPropagation(); }if (N.preventDefault){N.preventDefault(); }return false; } ,I0:function (){if (typeof(XMLHttpRequest)!="u\x6e\144\x65\x66ined"){ this.o1=new XMLHttpRequest(); }else if (typeof(ActiveXObject)!="undefined"){ this.o1=new ActiveXObject("\x4dicrosoft\x2e\x58MLH\x54\x54P"); }else return; this.o1.onreadystatechange=this.CreateEventHandler("\x48andleCal\x6c\x62ack"); this.o1.open("\x47\x45T",this.i3(), true); this.o1.send(""); } ,I3:function (U,method){return function (){method.apply(U,arguments); } ; } ,O0:function (d){var o4=d.indexOf("?")<0?"?": "&"; return d+o4+this.UniqueRequestIdentifier+"="+this.C; } ,i3:function (){return this.o0+"\x26RadUplo\x61\x64Time\x53\x74am\x70\x3d"+new Date().getTime(); } ,RegisterForSubmit:function (form){ this.O4(form); this.l4(form); } ,O4:function (form){var i4=this.CreateEventHandler("ClientSubm\x69\x74Han\x64\x6cer"); var I4=form.submit; try {form.submit= function (){if (i4()== false)return; form.submit=I4; form.submit(); };}catch (exception){try {var o5=__doPostBack; __doPostBack= function (eventTarget,eventArgument){var O5= true; if (typeof(Page_ClientValidate)=="functio\x6e"){O5=Page_ClientValidate(); }if (O5){if (i4()== false)return; o5(eventTarget,eventArgument); }} ; }catch (exception){}}} ,l4:function (form){ this.AttachDomEvent(form,"\163\x75\x62mit","ClientSub\x6d\x69tHan\x64\x6cer"); } ,B:function (form){if (typeof(form.action)=="\x75\x6edefine\x64")form.action=""; if (form.action.match(/\x3f/)){form.action=this.l5(form.action,this.UniqueRequestIdentifier); form.action=this.l5(form.action,this.EnableMemoryOptimizationIdentifier); if (form.action.substring(form.action.length-1)!="?"){form.action+="&"; }}else {form.action+="\x3f";}form.action+=this.UniqueRequestIdentifier+"\x3d"+this.C; if (this.EnableMemoryOptimization){form.enctype=form.encoding="mul\x74\x69part/f\x6f\x72m-d\x61\164a"; }else {form.action+="\x26"+this.EnableMemoryOptimizationIdentifier+"=fals\x65"; }form._initialAction=form.action; } ,l5:function (i5,I5){var o6=new RegExp("&?"+I5+"\x3d\x5b^&]*"); if (i5.match(o6)){return i5.replace(o6,""); }return i5; } ,FormatBytes:function (O6){var l6=O6/1024; var i6=l6/1024; if (i6>.8){return ""+Math.round(i6*100)/100+"M\x42"; }if (l6>.8){return ""+Math.round(l6*100)/100+"\x6bB"; }return ""+O6+"\x20bytes"; } ,GetFormattedTime:function (I6){var o7=this.NormalizeTime(I6); return this.TimeFormat.replace(/\x25\x48\x4f\x55\x52\x53\x25/,o7.O7).replace(/\x25\x4d\x49\x4e\x55\x54\x45\x53\x25/,o7.l7).replace(/\x25\x53\x45\x43\x4f\x4e\x44\x53\x25/,o7.i7); } ,NormalizeTime:function (I7){var I6=I7%60; var o8=Math.floor(I7/60); var O8=o8%60; var l8=Math.floor(o8/60); return {O7:l8,l7:O8,i7:I6 };}} ;;RadUploadNameSpace.RadUploadEventArgs= function (i8){ this.FileInputField=i8; } ; RadUploadNameSpace.RadUploadDeleteSelectedEventArgs= function (I8){ this.FileInputFields=I8; } ;;if (typeof(window.RadControlsNamespace)=="u\x6e\x64efined"){window.RadControlsNamespace=new Object(); } ; RadControlsNamespace.AppendStyleSheet= function (o9,K,O9){if (!O9){return; }if (!o9){document.write("<"+"\x6cink"+" rel=\047stylesh\x65et\047\x20type=\x27text/c\x73\x73\047\x20hre\x66=\047"+O9+"\x27 />"); }else {var l9=document.createElement("LI\x4e\113"); l9.rel="\163tyl\x65\x73heet"; l9.type="text/css"; l9.href=O9; document.getElementById(K+"StyleSheetH\x6f\x6cder").appendChild(l9); }} ;;if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
