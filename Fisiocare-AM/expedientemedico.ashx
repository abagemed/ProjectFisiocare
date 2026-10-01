
<%@ WebHandler Language="VB" Class="expedientemedico" %>

Imports System
Imports System.Web
Imports System.Data


Public Class expedientemedico : Implements IHttpHandler, IReadOnlySessionState
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim opcion As String = context.Request("opcion")
        Dim id As String = context.Request("id")
        Dim idcita As String = context.Request("idcita")
        Dim fecha As String = context.Request("fecha")
        Dim observaciones As String = context.Request("observaciones")
        Dim alergias As String = context.Request("alergias")
        Dim indicaciones_medicas As String = context.Request("indicaciones_medicas")
        Dim contra_indicaciones As String = context.Request("contra_indicaciones")
        Dim idcitafecha As String = context.Request("idcitafecha")
        Dim idcliente As String = context.Request("idcliente")
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String = ""
        Dim ultrasonido As String = context.Request("ultrasonido")
        Dim chc As String = context.Request("chc")
        Dim electroterapia As String = context.Request("electroterapia")
        Dim laser As String = context.Request("laser")
        Dim cf As String = context.Request("cf")
        Dim ejercicio As String = context.Request("ejercicio")
        Dim masaje As String = context.Request("masaje")
        Dim magneto As String = context.Request("magneto")
        Dim gimnasia As String = context.Request("gimnasia")
        Dim id_diagnostico As String = context.Request("id_diagnostico")
        Dim idH_diagnostico As String = context.Request("id")
        Dim id_protocolo As String = context.Request("id_protocolo")
        Dim idH_protocolo As String = context.Request("id")
        Dim fase As String = context.Request("fase")
        Dim data As String = ""
        Dim terapista As String = context.Request("terapista")
        Dim idterapista As String = context.Request("idterapista")
        Dim atendio As String = context.Request("atendio")
        Dim nombre_diagnostico As String = context.Request("nombre_diagnostico")
        Dim turno As String = context.Request("turno")
        
        'cambiosrealizado agregar campos y cambiar estructura AB03062020
        'Nuevas variables
        'Dim fecha As String = context.Request("fecha")
        'Dim terapista As String = context.Request("terapista")
        Dim exploracion_fisica As String = context.Request("exploracion_fisica")
        Dim exploracion_analgesia As String = context.Request("exploracion_analgesia")
        Dim exploracion_propiocepsion As String = context.Request("exploracion_propiocepsion")
        Dim exploracion_desinflamacion As String = context.Request("exploracion_desinflamacion")
        Dim exploracion_habilidades_manuales As String = context.Request("exploracion_habilidades_manuales")
        Dim exploracion_fortalecimiento As String = context.Request("exploracion_fortalecimiento")
        Dim exploracion_aumentar_rangos As String = context.Request("exploracion_aumentar_rangos")
        Dim exploracion_reduccion_marcha As String = context.Request("exploracion_reduccion_marcha")
        Dim exploracion_reintegracion_deportiva As String = context.Request("exploracion_reintegracion_deportiva")
        Dim analgesia_laser As String = context.Request("analgesia_laser")
        Dim analgesia_ultrasonido As String = context.Request("analgesia_ultrasonido")
        Dim analgesia_traccion_cervical As String = context.Request("analgesia_traccion_cervical")
        Dim analgesia_electroterapia As String = context.Request("analgesia_electroterapia")
        Dim analgesia_masaje As String = context.Request("analgesia_masaje")
        Dim analgesia_traccion_lumbar As String = context.Request("analgesia_traccion_lumbar")
        Dim analgesia_tape As String = context.Request("analgesia_tape")
        Dim analgesia_chc As String = context.Request("analgesia_chc")
        Dim analgesia_parafina As String = context.Request("analgesia_parafina")
        Dim analgesia_magneto As String = context.Request("analgesia_magneto")
        Dim analgesia_crio As String = context.Request("analgesia_crio")
        Dim analgesia_diatermia As String = context.Request("analgesia_diatermia")
        Dim analgesia_ondas As String = context.Request("analgesia_ondas")
        Dim analgesia_hidroterapia As String = context.Request("analgesia_hidroterapia")
        Dim analgesia_terapia_manual As String = context.Request("analgesia_terapia_manual")
        Dim analgesia_banios_contraste As String = context.Request("analgesia_banios_contraste")
        Dim analgesia_cf As String = context.Request("analgesia_cf")
        Dim analgesia_ejercicio As String = context.Request("analgesia_ejercicio")
        Dim analgesia_gimnasia As String = context.Request("analgesia_gimnasia")
        Dim estiramiento_sel As String = context.Request("estiramiento_sel")
        Dim estiramiento_activo_sup As String = context.Request("estiramiento_activo_sup")
        Dim estiramiento_pasivo_sup As String = context.Request("estiramiento_pasivo_sup")
        Dim estiramiento_cintura_escapular As String = context.Request("estiramiento_cintura_escapular")
        Dim estiramiento_extensores_muneca As String = context.Request("estiramiento_extensores_muneca")
        Dim estiramiento_extensores_codo As String = context.Request("estiramiento_extensores_codo")
        Dim estiramiento_flexores_muneca As String = context.Request("estiramiento_flexores_muneca")
        Dim estiramiento_flexores_codo As String = context.Request("estiramiento_flexores_codo")
        Dim estiramiento_maq_mov_pasiva_hombro As String = context.Request("estiramiento_maq_mov_pasiva_hombro")
        Dim estiramiento_mano_hombro As String = context.Request("estiramiento_mano_hombro")
        Dim estiramiento_superior_3_5 As String = context.Request("estiramiento_superior_3_5")
        Dim estiramiento_superior_3_10 As String = context.Request("estiramiento_superior_3_10")
        Dim estiramiento_superior_3_15 As String = context.Request("estiramiento_superior_3_15")
        Dim estiramiento_dedos As String = context.Request("estiramiento_dedos")
        Dim estiramiento_otro_superior As String = context.Request("estiramiento_otro_superior")
        Dim estiramiento_otro_columna As String = context.Request("estiramiento_otro_columna")
        Dim estiramiento_otro_inferior As String = context.Request("estiramiento_otro_inferior")
        Dim estiramiento_activo_columna As String = context.Request("estiramiento_activo_columna")
        Dim estiramiento_pasivo_columna As String = context.Request("estiramiento_pasivo_columna")
        Dim estiramiento_paravertebrales_dorsales As String = context.Request("estiramiento_paravertebrales_dorsales")
        Dim estiramiento_paravertebrales_lumbares As String = context.Request("estiramiento_paravertebrales_lumbares")
        Dim estiramiento_cervicales As String = context.Request("estiramiento_cervicales")
        Dim estiramiento_paravertebrales As String = context.Request("estiramiento_paravertebrales")
        Dim estiramiento_columna_3_5 As String = context.Request("estiramiento_columna_3_5")
        Dim estiramiento_columna_3_10 As String = context.Request("estiramiento_columna_3_10")
        Dim estiramiento_columna_3_15 As String = context.Request("estiramiento_columna_3_15")
        Dim estiramiento_trapecio As String = context.Request("estiramiento_trapecio")
        Dim estiramiento_activo_inf As String = context.Request("estiramiento_activo_inf")
        Dim estiramiento_pasivo_inf As String = context.Request("estiramiento_pasivo_inf")
        Dim estiramiento_cuadriceps As String = context.Request("estiramiento_cuadriceps")
        Dim estiramiento_tensor_fascia_lata As String = context.Request("estiramiento_tensor_fascia_lata")
        Dim estiramiento_isquiotibiales As String = context.Request("estiramiento_isquiotibiales")
        Dim estiramiento_tibial_anterior As String = context.Request("estiramiento_tibial_anterior")
        Dim estiramiento_aductores As String = context.Request("estiramiento_aductores")
        Dim estiramiento_tibial_posterior As String = context.Request("estiramiento_tibial_posterior")
        Dim estiramiento_abductores As String = context.Request("estiramiento_abductores")
        Dim estiramiento_peroneos As String = context.Request("estiramiento_peroneos")
        Dim estiramiento_rotadores_cadera As String = context.Request("estiramiento_rotadores_cadera")
        Dim estiramiento_maq_mov_pasiva_rodilla As String = context.Request("estiramiento_maq_mov_pasiva_rodilla")
        Dim estiramiento_fascia_plantar As String = context.Request("estiramiento_fascia_plantar")
        Dim estiramiento_maq_mov_pasiva_tobillo As String = context.Request("estiramiento_maq_mov_pasiva_tobillo")
        Dim estiramiento_inferior_3_5 As String = context.Request("estiramiento_inferior_3_5")
        Dim estiramiento_inferior_3_10 As String = context.Request("estiramiento_inferior_3_10")
        Dim estiramiento_inferior_3_15 As String = context.Request("estiramiento_inferior_3_15")
        Dim fortalecimiento_sel As String = context.Request("fortalecimiento_sel")
        Dim fortalecimiento_cintura_escapular As String = context.Request("fortalecimiento_cintura_escapular")
        Dim fortalecimiento_ligas As String = context.Request("fortalecimiento_ligas")
        Dim fortalecimiento_polainas As String = context.Request("fortalecimiento_polainas")
        Dim fortalecimiento_isometricas As String = context.Request("fortalecimiento_isometricas")
        Dim fortalecimiento_sin_peso As String = context.Request("fortalecimiento_sin_peso")
        Dim fortalecimiento_extensores_muneca As String = context.Request("fortalecimiento_extensores_muneca")
        Dim fortalecimiento_extensores_codo As String = context.Request("fortalecimiento_extensores_codo")
        Dim fortalecimiento_flexores_muneca As String = context.Request("fortalecimiento_flexores_muneca")
        Dim fortalecimiento_flexores_codo As String = context.Request("fortalecimiento_flexores_codo")
        Dim fortalecimiento_maq_mov_pasiva_hombro As String = context.Request("fortalecimiento_maq_mov_pasiva_hombro")
        Dim fortalecimiento_mano_hombro As String = context.Request("fortalecimiento_mano_hombro")
        Dim fortalecimiento_superior_3_5 As String = context.Request("fortalecimiento_superior_3_5")
        Dim fortalecimiento_superior_3_10 As String = context.Request("fortalecimiento_superior_3_10")
        Dim fortalecimiento_superior_3_15 As String = context.Request("fortalecimiento_superior_3_15")
        Dim fortalecimiento_dedos As String = context.Request("fortalecimiento_dedos")
        Dim fortalecimiento_otro_superior As String = context.Request("fortalecimiento_otro_superior")
        Dim fortalecimiento_otro_columna As String = context.Request("fortalecimiento_otro_columna")
        Dim fortalecimiento_otro_inferior As String = context.Request("fortalecimiento_otro_inferior")
        Dim fortalecimiento_paravertebrales_dorsales As String = context.Request("fortalecimiento_paravertebrales_dorsales")
        Dim fortalecimiento_williams As String = context.Request("fortalecimiento_williams")
        Dim fortalecimiento_mckenzic As String = context.Request("fortalecimiento_mckenzic")
        Dim fortalecimiento_core As String = context.Request("fortalecimiento_core")
        Dim fortalecimiento_klapp As String = context.Request("fortalecimiento_klapp")
        Dim fortalecimiento_paravertebrales_lumbares As String = context.Request("fortalecimiento_paravertebrales_lumbares")
        Dim fortalecimiento_cervicales As String = context.Request("fortalecimiento_cervicales")
        Dim fortalecimiento_paravertebrales As String = context.Request("fortalecimiento_paravertebrales")
        Dim fortalecimiento_columna_3_5 As String = context.Request("fortalecimiento_columna_3_5")
        Dim fortalecimiento_columna_3_10 As String = context.Request("fortalecimiento_columna_3_10")
        Dim fortalecimiento_columna_3_15 As String = context.Request("fortalecimiento_columna_3_15")
        Dim fortalecimiento_trapecio As String = context.Request("fortalecimiento_trapecio")
        Dim fortalecimiento_ligas_columna As String = context.Request("fortalecimiento_ligas_columna")
        Dim fortalecimiento_isometricas_columna As String = context.Request("fortalecimiento_isometricas_columna")
        Dim fortalecimiento_cuadriceps As String = context.Request("fortalecimiento_cuadriceps")
        Dim fortalecimiento_ligas_inf As String = context.Request("fortalecimiento_ligas_inf")
        Dim fortalecimiento_isometricas_inf As String = context.Request("fortalecimiento_isometricas_inf")
        Dim fortalecimiento_tensor_fascia_lata As String = context.Request("fortalecimiento_tensor_fascia_lata")
        Dim fortalecimiento_isquiotibiales As String = context.Request("fortalecimiento_isquiotibiales")
        Dim fortalecimiento_tibial_anterior As String = context.Request("fortalecimiento_tibial_anterior")
        Dim fortalecimiento_aductores As String = context.Request("fortalecimiento_aductores")
        Dim fortalecimiento_tibial_posterior As String = context.Request("fortalecimiento_tibial_posterior")
        Dim fortalecimiento_abductores As String = context.Request("fortalecimiento_abductores")
        Dim fortalecimiento_peroneos As String = context.Request("fortalecimiento_peroneos")
        Dim fortalecimiento_rotadores_cadera As String = context.Request("fortalecimiento_rotadores_cadera")
        Dim fortalecimiento_fascia_plantar As String = context.Request("fortalecimiento_fascia_plantar")
        Dim fortalecimiento_polainas_inf As String = context.Request("fortalecimiento_polainas_inf")
        Dim fortalecimiento_sin_peso_inf As String = context.Request("fortalecimiento_sin_peso_inf")
        Dim fortalecimiento_banco As String = context.Request("fortalecimiento_banco")
        Dim fortalecimiento_trampolin As String = context.Request("fortalecimiento_trampolin")
        Dim fortalecimiento_bossu As String = context.Request("fortalecimiento_bossu")
        Dim fortalecimiento_inferior_3_5 As String = context.Request("fortalecimiento_inferior_3_5")
        Dim fortalecimiento_inferior_3_10 As String = context.Request("fortalecimiento_inferior_3_10")
        Dim fortalecimiento_inferior_3_15 As String = context.Request("fortalecimiento_inferior_3_15")
        Dim reacondicionamiento_bici As String = context.Request("reacondicionamiento_bici")
        Dim reacondicionamiento_tiempo_bici As String = context.Request("reacondicionamiento_tiempo_bici")
        Dim reacondicionamiento_caminadora As String = context.Request("reacondicionamiento_caminadora")
        Dim reacondicionamiento_tiempo_caminadora As String = context.Request("reacondicionamiento_tiempo_caminadora")
        Dim reacondicionamiento_reduccion_marcha As String = context.Request("reacondicionamiento_reduccion_marcha")
        Dim reacondicionamiento_eliptica As String = context.Request("reacondicionamiento_eliptica")
        Dim reacondicionamiento_escaleras As String = context.Request("reacondicionamiento_escaleras")
        Dim reacondicionamiento_pelotas As String = context.Request("reacondicionamiento_pelotas")
        Dim miembro_superior As String = context.Request("miembro_superior")
        Dim columna As String = context.Request("columna")
        Dim miembro_inferior As String = context.Request("miembro_inferior")
        Dim analgesia_laser_otro As String = context.Request("analgesia_laser_otro")
        Dim analgesia_ultrasonido_otro As String = context.Request("analgesia_ultrasonido_otro")
        Dim analgesia_electroterapia_otro As String = context.Request("analgesia_electroterapia_otro")
        
        
        
        'pruebas
        fortalecimiento_sel = 3
        exploracion_analgesia = 1
        exploracion_desinflamacion = 1
        
        
        
        
        If opcion = 1 Then
            'Leer datos
            data = devolverDatospaciente(idcita, idcitafecha, turno)
            'data = "[{""exploracion_analgesia"" : """ & exploracion_analgesia & """,""exploracion_propiocepsion"" : """ & exploracion_propiocepsion & """,""exploracion_desinflamacion"" : """ & exploracion_desinflamacion & """,""exploracion_habilidades_manuales"" : """ & exploracion_habilidades_manuales & """,""exploracion_fortalecimiento"" : """ & exploracion_fortalecimiento & """,""exploracion_aumentar_rangos"" : """ & exploracion_aumentar_rangos & """,""exploracion_reduccion_marcha"" : """ & exploracion_reduccion_marcha & """,""exploracion_reintegracion_deportiva"" : """ & exploracion_reintegracion_deportiva & """,""analgesia_laser"" : """ & analgesia_laser & """,""analgesia_ultrasonido"" : """ & analgesia_ultrasonido & """,""analgesia_traccion_cervical"" : """ & analgesia_traccion_cervical & """,""analgesia_electroterapia"" : """ & analgesia_electroterapia & """,""analgesia_masaje"" : """ & analgesia_masaje & """,""analgesia_traccion_lumbar"" : """ & analgesia_traccion_lumbar & """,""analgesia_tape"" : """ & analgesia_tape & """,""analgesia_chc"" : """ & analgesia_chc & """,""analgesia_parafina"" : """ & analgesia_parafina & """,""analgesia_magneto"" : """ & analgesia_magneto & """,""analgesia_crio"" : """ & analgesia_crio & """,""analgesia_diatermia"" : """ & analgesia_diatermia & """,""analgesia_ondas"" : """ & analgesia_ondas & """,""analgesia_hidroterapia"" : """ & analgesia_hidroterapia & """,""analgesia_terapia_manual"" : """ & analgesia_terapia_manual & """,""analgesia_banios_contraste"" : """ & analgesia_banios_contraste & """,""analgesia_cf"" : """ & analgesia_cf & """,""analgesia_ejercicio"" : """ & analgesia_ejercicio & """,""analgesia_gimnasia"" : """ & analgesia_gimnasia & """,""estiramiento_sel"" : """ & estiramiento_sel & """,""estiramiento_activo_sup"" : """ & estiramiento_activo_sup & """,""estiramiento_pasivo_sup"" : """ & estiramiento_pasivo_sup & """,""estiramiento_cintura_escapular"" : """ & estiramiento_cintura_escapular & """,""estiramiento_extensores_muneca"" : """ & estiramiento_extensores_muneca & """,""estiramiento_extensores_codo"" : """ & estiramiento_extensores_codo & """,""estiramiento_flexores_muneca"" : """ & estiramiento_flexores_muneca & """,""estiramiento_flexores_codo"" : """ & estiramiento_flexores_codo & """,""estiramiento_maq_mov_pasiva_hombro"" : """ & estiramiento_maq_mov_pasiva_hombro & """,""estiramiento_mano_hombro"" : """ & estiramiento_mano_hombro & """,""estiramiento_superior_3_5"" : """ & estiramiento_superior_3_5 & """,""estiramiento_superior_3_10"" : """ & estiramiento_superior_3_10 & """,""estiramiento_superior_3_15"" : """ & estiramiento_superior_3_15 & """,""estiramiento_dedos"" : """ & estiramiento_dedos & """,""estiramiento_otro_estiramiento"" : """ & estiramiento_otro_estiramiento & """,""estiramiento_activo_columna"" : """ & estiramiento_activo_columna & """,""estiramiento_pasivo_columna"" : """ & estiramiento_pasivo_columna & """,""estiramiento_paravertebrales_dorsales"" : """ & estiramiento_paravertebrales_dorsales & """,""estiramiento_paravertebrales_lumbares"" : """ & estiramiento_paravertebrales_lumbares & """,""estiramiento_cervicales"" : """ & estiramiento_cervicales & """,""estiramiento_paravertebrales"" : """ & estiramiento_paravertebrales & """,""estiramiento_columna_3_5"" : """ & estiramiento_columna_3_5 & """,""estiramiento_columna_3_10"" : """ & estiramiento_columna_3_10 & """,""estiramiento_columna_3_15"" : """ & estiramiento_columna_3_15 & """,""estiramiento_trapecio"" : """ & estiramiento_trapecio & """,""estiramiento_activo_inf"" : """ & estiramiento_activo_inf & """,""estiramiento_pasivo_inf"" : """ & estiramiento_pasivo_inf & """,""estiramiento_cuadriceps"" : """ & estiramiento_cuadriceps & """,""estiramiento_tensor_fascia_lata"" : """ & estiramiento_tensor_fascia_lata & """,""estiramiento_isquiotibiales"" : """ & estiramiento_isquiotibiales & """,""estiramiento_tibial_anterior"" : """ & estiramiento_tibial_anterior & """,""estiramiento_aductores"" : """ & estiramiento_aductores & """,""estiramiento_tibial_posterior"" : """ & estiramiento_tibial_posterior & """,""estiramiento_abductores"" : """ & estiramiento_abductores & """,""estiramiento_peroneos"" : """ & estiramiento_peroneos & """,""estiramiento_rotadores_cadera"" : """ & estiramiento_rotadores_cadera & """,""estiramiento_maq_mov_pasiva_rodilla"" : """ & estiramiento_maq_mov_pasiva_rodilla & """,""estiramiento_fascia_plantar"" : """ & estiramiento_fascia_plantar & """,""estiramiento_maq_mov_pasiva_tobillo"" : """ & estiramiento_maq_mov_pasiva_tobillo & """,""estiramiento_inferior_3_5"" : """ & estiramiento_inferior_3_5 & """,""estiramiento_inferior_3_10"" : """ & estiramiento_inferior_3_10 & """,""estiramiento_inferior_3_15"" : """ & estiramiento_inferior_3_15 & """,""fortalecimiento_sel"" : """ & fortalecimiento_sel & """,""fortalecimiento_cintura_escapular"" : """ & fortalecimiento_cintura_escapular & """,""fortalecimiento_ligas"" : """ & fortalecimiento_ligas & """,""fortalecimiento_polainas"" : """ & fortalecimiento_polainas & """,""fortalecimiento_isometricas"" : """ & fortalecimiento_isometricas & """,""fortalecimiento_sin_peso"" : """ & fortalecimiento_sin_peso & """,""fortalecimiento_extensores_muneca"" : """ & fortalecimiento_extensores_muneca & """,""fortalecimiento_extensores_codo"" : """ & fortalecimiento_extensores_codo & """,""fortalecimiento_flexores_muneca"" : """ & fortalecimiento_flexores_muneca & """,""fortalecimiento_flexores_codo"" : """ & fortalecimiento_flexores_codo & """,""fortalecimiento_maq_mov_pasiva_hombro"" : """ & fortalecimiento_maq_mov_pasiva_hombro & """,""fortalecimiento_mano_hombro"" : """ & fortalecimiento_mano_hombro & """,""fortalecimiento_superior_3_5"" : """ & fortalecimiento_superior_3_5 & """,""fortalecimiento_superior_3_10"" : """ & fortalecimiento_superior_3_10 & """,""fortalecimiento_superior_3_15"" : """ & fortalecimiento_superior_3_15 & """,""fortalecimiento_dedos"" : """ & fortalecimiento_dedos & """,""fortalecimiento_otro_estiramiento"" : """ & fortalecimiento_otro_estiramiento & """,""fortalecimiento_paravertebrales_dorsales"" : """ & fortalecimiento_paravertebrales_dorsales & """,""fortalecimiento_williams"" : """ & fortalecimiento_williams & """,""fortalecimiento_mckenzic"" : """ & fortalecimiento_mckenzic & """,""fortalecimiento_core"" : """ & fortalecimiento_core & """,""fortalecimiento_klapp"" : """ & fortalecimiento_klapp & """,""fortalecimiento_paravertebrales_lumbares"" : """ & fortalecimiento_paravertebrales_lumbares & """,""fortalecimiento_cervicales"" : """ & fortalecimiento_cervicales & """,""fortalecimiento_paravertebrales"" : """ & fortalecimiento_paravertebrales & """,""fortalecimiento_columna_3_5"" : """ & fortalecimiento_columna_3_5 & """,""fortalecimiento_columna_3_10"" : """ & fortalecimiento_columna_3_10 & """,""fortalecimiento_columna_3_15"" : """ & fortalecimiento_columna_3_15 & """,""fortalecimiento_trapecio"" : """ & fortalecimiento_trapecio & """,""fortalecimiento_ligas_columna"" : """ & fortalecimiento_ligas_columna & """,""fortalecimiento_isometricas_columna"" : """ & fortalecimiento_isometricas_columna & """,""fortalecimiento_cuadriceps"" : """ & fortalecimiento_cuadriceps & """,""fortalecimiento_ligas_inf"" : """ & fortalecimiento_ligas_inf & """,""fortalecimiento_isometricas_inf"" : """ & fortalecimiento_isometricas_inf & """,""fortalecimiento_tensor_fascia_lata"" : """ & fortalecimiento_tensor_fascia_lata & """,""fortalecimiento_isquiotibiales"" : """ & fortalecimiento_isquiotibiales & """,""fortalecimiento_tibial_anterior"" : """ & fortalecimiento_tibial_anterior & """,""fortalecimiento_aductores"" : """ & fortalecimiento_aductores & """,""fortalecimiento_tibial_posterior"" : """ & fortalecimiento_tibial_posterior & """,""fortalecimiento_abductores"" : """ & fortalecimiento_abductores & """,""fortalecimiento_peroneos"" : """ & fortalecimiento_peroneos & """,""fortalecimiento_rotadores_cadera"" : """ & fortalecimiento_rotadores_cadera & """,""fortalecimiento_fascia_plantar"" : """ & fortalecimiento_fascia_plantar & """,""fortalecimiento_polainas_inf"" : """ & fortalecimiento_polainas_inf & """,""fortalecimiento_sin_peso_inf"" : """ & fortalecimiento_sin_peso_inf & """,""fortalecimiento_banco"" : """ & fortalecimiento_banco & """,""fortalecimiento_trampolin"" : """ & fortalecimiento_trampolin & """,""fortalecimiento_bossu"" : """ & fortalecimiento_bossu & """,""fortalecimiento_inferior_3_5"" : """ & fortalecimiento_inferior_3_5 & """,""fortalecimiento_inferior_3_10"" : """ & fortalecimiento_inferior_3_10 & """,""fortalecimiento_inferior_3_15"" : """ & fortalecimiento_inferior_3_15 & """,""reacondicionamiento_bici"" : """ & reacondicionamiento_bici & """,""reacondicionamiento_tiempo_bici"" : """ & reacondicionamiento_tiempo_bici & """,""reacondicionamiento_caminadora"" : """ & reacondicionamiento_caminadora & """,""reacondicionamiento_tiempo_caminadora"" : """ & reacondicionamiento_tiempo_caminadora & """,""reacondicionamiento_reduccion_marcha"" : """ & reacondicionamiento_reduccion_marcha & """,""reacondicionamiento_eliptica"" : """ & reacondicionamiento_eliptica & """,""reacondicionamiento_escaleras"" : """ & reacondicionamiento_escaleras & """,""reacondicionamiento_pelotas"" : """ & reacondicionamiento_pelotas & """}]"
            
        ElseIf opcion = 2 Then
            'Guardar datos terapias
            
            'AlegiasPadecimientos
            strSQL = " select  IDAntecendetes FROM [HMAntecedentesMedicos]  where idcliente=" & idcliente & " "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'update
                    strSQL = "update  [HMAntecedentesMedicos]  set AlegiasPadecimientos='" & alergias & "' where idcliente=" & idcliente & " "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = devolverDatospaciente(idcita, idcitafecha, turno)
                    Else
                        'error
                        'data = "{""resp"" : ""1""}"
                    End If
                Else
                    'insert
                    strSQL = " insert into [HMAntecedentesMedicos] ( idCliente, AlegiasPadecimientos, FechaActualizacion) " & _
                    "VALUES(" & idcliente & ",'" & alergias & "',GETDATE()) "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = devolverDatospaciente(idcita, idcitafecha, turno)
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            
            
            'IMedicas, CIndicaciones
            
            strSQL = " select top 1 idTerapia FROM [Terapia]  where idcliente=" & idcliente & " order by idTerapia desc "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'update
                    strSQL = "update  [Terapia]  set IMedicas='" & indicaciones_medicas & "',CIndicaciones='" & contra_indicaciones & "' where idterapia=" & (dt.Rows(0).Item("idTerapia")) & " "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = devolverDatospaciente(idcita, idcitafecha, turno)
                       
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                Else
                    'insert
                    strSQL = " insert into [Terapia] (idcliente, muscular, basica, laser, consulta, movimiento, adicional, hospital,IMedicas, CIndicaciones) " & _
                    "VALUES(" & idcliente & ",'False','False','False','False','False','False','False','" & indicaciones_medicas & "','" & contra_indicaciones & "') "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = devolverDatospaciente(idcita, idcitafecha, turno)
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            
            
            
            'Guardar datos tratamientos
            strSQL = " select idtratamiento FROM [HMTratamientos]  where idcliente=" & idcliente & " and idcita =" & idcitafecha & "  "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'update
                    'strSQL = "update  [HMTratamientos]  set ultrasonido=" & ultrasonido & ",chc=" & chc & " ,electroterapia=" & electroterapia & " ,laser=" & laser & " ,cf=" & cf & " ,ejercicio=" & ejercicio & " ,masaje=" & masaje & " ,magneto=" & magneto & " ,gimnasia=" & gimnasia & ",observaciones='" & observaciones & "',idatendio='" & idterapista & "'   where idcliente=" & idcliente & " and idcita =" & idcitafecha & "  "
                    strSQL = " update  " & clsDatos.BaseDatos & ".dbo.HMTratamientos set  " & _
                   " exploracion_fisica='" & exploracion_fisica & "' ," & _
                   " exploracion_analgesia='" & exploracion_analgesia & "' ," & _
                   " exploracion_propiocepsion='" & exploracion_propiocepsion & "' ," & _
                   " exploracion_desinflamacion='" & exploracion_desinflamacion & "' ," & _
                   " exploracion_habilidades_manuales='" & exploracion_habilidades_manuales & "' ," & _
                   " exploracion_fortalecimiento='" & exploracion_fortalecimiento & "' ," & _
                   " exploracion_aumentar_rangos='" & exploracion_aumentar_rangos & "' ," & _
                   " exploracion_reduccion_marcha='" & exploracion_reduccion_marcha & "' ," & _
                   " exploracion_reintegracion_deportiva='" & exploracion_reintegracion_deportiva & "' ," & _
                   " analgesia_laser='" & analgesia_laser & "' ," & _
                   " analgesia_ultrasonido='" & analgesia_ultrasonido & "' ," & _
                   " analgesia_traccion_cervical='" & analgesia_traccion_cervical & "' ," & _
                   " analgesia_electroterapia='" & analgesia_electroterapia & "' ," & _
                   " analgesia_masaje='" & analgesia_masaje & "' ," & _
                   " analgesia_traccion_lumbar='" & analgesia_traccion_lumbar & "' ," & _
                   " analgesia_tape='" & analgesia_tape & "' ," & _
                   " analgesia_chc='" & analgesia_chc & "' ," & _
                   " analgesia_parafina='" & analgesia_parafina & "' ," & _
                   " analgesia_magneto='" & analgesia_magneto & "' ," & _
                   " analgesia_crio='" & analgesia_crio & "' ," & _
                   " analgesia_diatermia='" & analgesia_diatermia & "' ," & _
                   " analgesia_ondas='" & analgesia_ondas & "' ," & _
                   " analgesia_hidroterapia='" & analgesia_hidroterapia & "' ," & _
                   " analgesia_terapia_manual='" & analgesia_terapia_manual & "' ," & _
                   " analgesia_banios_contraste='" & analgesia_banios_contraste & "' ," & _
                   " analgesia_cf='" & analgesia_cf & "' ," & _
                   " analgesia_ejercicio='" & analgesia_ejercicio & "' ," & _
                   " analgesia_gimnasia='" & analgesia_gimnasia & "' ," & _
                   " estiramiento_sel='" & estiramiento_sel & "' ," & _
                   " miembro_superior='" & miembro_superior & "' ," & _
                   " columna='" & columna & "' ," & _
                   " miembro_inferior='" & miembro_inferior & "' ," & _
                   " estiramiento_activo_sup='" & estiramiento_activo_sup & "' ," & _
                   " estiramiento_pasivo_sup='" & estiramiento_pasivo_sup & "' ," & _
                   " estiramiento_cintura_escapular='" & estiramiento_cintura_escapular & "' ," & _
                   " estiramiento_extensores_muneca='" & estiramiento_extensores_muneca & "' ," & _
                   " estiramiento_extensores_codo='" & estiramiento_extensores_codo & "' ," & _
                   " estiramiento_flexores_muneca='" & estiramiento_flexores_muneca & "' ," & _
                   " estiramiento_flexores_codo='" & estiramiento_flexores_codo & "' ," & _
                   " estiramiento_maq_mov_pasiva_hombro='" & estiramiento_maq_mov_pasiva_hombro & "' ," & _
                   " estiramiento_mano_hombro='" & estiramiento_mano_hombro & "' ," & _
                   " estiramiento_superior_3_5='" & estiramiento_superior_3_5 & "' ," & _
                   " estiramiento_superior_3_10='" & estiramiento_superior_3_10 & "' ," & _
                   " estiramiento_superior_3_15='" & estiramiento_superior_3_15 & "' ," & _
                   " estiramiento_dedos='" & estiramiento_dedos & "' ," & _
                   " estiramiento_otro_superior='" & estiramiento_otro_superior & "' ," & _
                   " estiramiento_otro_columna='" & estiramiento_otro_columna & "' ," & _
                   " estiramiento_otro_inferior='" & estiramiento_otro_inferior & "' ," & _
                   " estiramiento_activo_columna='" & estiramiento_activo_columna & "' ," & _
                   " estiramiento_pasivo_columna='" & estiramiento_pasivo_columna & "' ," & _
                   " estiramiento_paravertebrales_dorsales='" & estiramiento_paravertebrales_dorsales & "' ," & _
                   " estiramiento_paravertebrales_lumbares='" & estiramiento_paravertebrales_lumbares & "' ," & _
                   " estiramiento_cervicales='" & estiramiento_cervicales & "' ," & _
                   " estiramiento_paravertebrales='" & estiramiento_paravertebrales & "' ," & _
                   " estiramiento_columna_3_5='" & estiramiento_columna_3_5 & "' ," & _
                   " estiramiento_columna_3_10='" & estiramiento_columna_3_10 & "' ," & _
                   " estiramiento_columna_3_15='" & estiramiento_columna_3_15 & "' ," & _
                   " estiramiento_trapecio='" & estiramiento_trapecio & "' ," & _
                   " estiramiento_activo_inf='" & estiramiento_activo_inf & "' ," & _
                   " estiramiento_pasivo_inf='" & estiramiento_pasivo_inf & "' ," & _
                   " estiramiento_cuadriceps='" & estiramiento_cuadriceps & "' ," & _
                   " estiramiento_tensor_fascia_lata='" & estiramiento_tensor_fascia_lata & "' ," & _
                   " estiramiento_isquiotibiales='" & estiramiento_isquiotibiales & "' ," & _
                   " estiramiento_tibial_anterior='" & estiramiento_tibial_anterior & "' ," & _
                   " estiramiento_aductores='" & estiramiento_aductores & "' ," & _
                   " estiramiento_tibial_posterior='" & estiramiento_tibial_posterior & "' ," & _
                   " estiramiento_abductores='" & estiramiento_abductores & "' ," & _
                   " estiramiento_peroneos='" & estiramiento_peroneos & "' ," & _
                   " estiramiento_rotadores_cadera='" & estiramiento_rotadores_cadera & "' ," & _
                   " estiramiento_maq_mov_pasiva_rodilla='" & estiramiento_maq_mov_pasiva_rodilla & "' ," & _
                   " estiramiento_fascia_plantar='" & estiramiento_fascia_plantar & "' ," & _
                   " estiramiento_maq_mov_pasiva_tobillo='" & estiramiento_maq_mov_pasiva_tobillo & "' ," & _
                   " estiramiento_inferior_3_5='" & estiramiento_inferior_3_5 & "' ," & _
                   " estiramiento_inferior_3_10='" & estiramiento_inferior_3_10 & "' ," & _
                   " estiramiento_inferior_3_15='" & estiramiento_inferior_3_15 & "' ," & _
                   " fortalecimiento_sel='" & fortalecimiento_sel & "' ," & _
                   " fortalecimiento_cintura_escapular='" & fortalecimiento_cintura_escapular & "' ," & _
                   " fortalecimiento_ligas='" & fortalecimiento_ligas & "' ," & _
                   " fortalecimiento_polainas='" & fortalecimiento_polainas & "' ," & _
                   " fortalecimiento_isometricas='" & fortalecimiento_isometricas & "' ," & _
                   " fortalecimiento_sin_peso='" & fortalecimiento_sin_peso & "' ," & _
                   " fortalecimiento_extensores_muneca='" & fortalecimiento_extensores_muneca & "' ," & _
                   " fortalecimiento_extensores_codo='" & fortalecimiento_extensores_codo & "' ," & _
                   " fortalecimiento_flexores_muneca='" & fortalecimiento_flexores_muneca & "' ," & _
                   " fortalecimiento_flexores_codo='" & fortalecimiento_flexores_codo & "' ," & _
                   " fortalecimiento_maq_mov_pasiva_hombro='" & fortalecimiento_maq_mov_pasiva_hombro & "' ," & _
                   " fortalecimiento_mano_hombro='" & fortalecimiento_mano_hombro & "' ," & _
                   " fortalecimiento_superior_3_5='" & fortalecimiento_superior_3_5 & "' ," & _
                   " fortalecimiento_superior_3_10='" & fortalecimiento_superior_3_10 & "' ," & _
                   " fortalecimiento_superior_3_15='" & fortalecimiento_superior_3_15 & "' ," & _
                   " fortalecimiento_dedos='" & fortalecimiento_dedos & "' ," & _
                   " fortalecimiento_otro_superior='" & fortalecimiento_otro_superior & "' ," & _
                   " fortalecimiento_otro_columna='" & fortalecimiento_otro_columna & "' ," & _
                   " fortalecimiento_otro_inferior='" & fortalecimiento_otro_inferior & "' ," & _
                   " fortalecimiento_paravertebrales_dorsales='" & fortalecimiento_paravertebrales_dorsales & "' ," & _
                   " fortalecimiento_williams='" & fortalecimiento_williams & "' ," & _
                   " fortalecimiento_mckenzic='" & fortalecimiento_mckenzic & "' ," & _
                   " fortalecimiento_core='" & fortalecimiento_core & "' ," & _
                   " fortalecimiento_klapp='" & fortalecimiento_klapp & "' ," & _
                   " fortalecimiento_paravertebrales_lumbares='" & fortalecimiento_paravertebrales_lumbares & "' ," & _
                   " fortalecimiento_cervicales='" & fortalecimiento_cervicales & "' ," & _
                   " fortalecimiento_paravertebrales='" & fortalecimiento_paravertebrales & "' ," & _
                   " fortalecimiento_columna_3_5='" & fortalecimiento_columna_3_5 & "' ," & _
                   " fortalecimiento_columna_3_10='" & fortalecimiento_columna_3_10 & "' ," & _
                   " fortalecimiento_columna_3_15='" & fortalecimiento_columna_3_15 & "' ," & _
                   " fortalecimiento_trapecio='" & fortalecimiento_trapecio & "' ," & _
                   " fortalecimiento_ligas_columna='" & fortalecimiento_ligas_columna & "' ," & _
                   " fortalecimiento_isometricas_columna='" & fortalecimiento_isometricas_columna & "' ," & _
                   " fortalecimiento_cuadriceps='" & fortalecimiento_cuadriceps & "' ," & _
                   " fortalecimiento_ligas_inf='" & fortalecimiento_ligas_inf & "' ," & _
                   " fortalecimiento_isometricas_inf='" & fortalecimiento_isometricas_inf & "' ," & _
                   " fortalecimiento_tensor_fascia_lata='" & fortalecimiento_tensor_fascia_lata & "' ," & _
                   " fortalecimiento_isquiotibiales='" & fortalecimiento_isquiotibiales & "' ," & _
                   " fortalecimiento_tibial_anterior='" & fortalecimiento_tibial_anterior & "' ," & _
                   " fortalecimiento_aductores='" & fortalecimiento_aductores & "' ," & _
                   " fortalecimiento_tibial_posterior='" & fortalecimiento_tibial_posterior & "' ," & _
                   " fortalecimiento_abductores='" & fortalecimiento_abductores & "' ," & _
                   " fortalecimiento_peroneos='" & fortalecimiento_peroneos & "' ," & _
                   " fortalecimiento_rotadores_cadera='" & fortalecimiento_rotadores_cadera & "' ," & _
                   " fortalecimiento_fascia_plantar='" & fortalecimiento_fascia_plantar & "' ," & _
                   " fortalecimiento_polainas_inf='" & fortalecimiento_polainas_inf & "' ," & _
                   " fortalecimiento_sin_peso_inf='" & fortalecimiento_sin_peso_inf & "' ," & _
                   " fortalecimiento_banco='" & fortalecimiento_banco & "' ," & _
                   " fortalecimiento_trampolin='" & fortalecimiento_trampolin & "' ," & _
                   " fortalecimiento_bossu='" & fortalecimiento_bossu & "' ," & _
                   " fortalecimiento_inferior_3_5='" & fortalecimiento_inferior_3_5 & "' ," & _
                   " fortalecimiento_inferior_3_10='" & fortalecimiento_inferior_3_10 & "' ," & _
                   " fortalecimiento_inferior_3_15='" & fortalecimiento_inferior_3_15 & "' ," & _
                   " reacondicionamiento_bici='" & reacondicionamiento_bici & "' ," & _
                   " reacondicionamiento_tiempo_bici='" & reacondicionamiento_tiempo_bici & "' ," & _
                   " reacondicionamiento_caminadora='" & reacondicionamiento_caminadora & "' ," & _
                   " reacondicionamiento_tiempo_caminadora='" & reacondicionamiento_tiempo_caminadora & "' ," & _
                   " reacondicionamiento_reduccion_marcha='" & reacondicionamiento_reduccion_marcha & "' ," & _
                   " reacondicionamiento_eliptica='" & reacondicionamiento_eliptica & "' ," & _
                   " reacondicionamiento_escaleras='" & reacondicionamiento_escaleras & "' ," & _
                   " reacondicionamiento_pelotas='" & reacondicionamiento_pelotas & "' ," & _
                   " observaciones='" & observaciones & "' ," & _
                   " analgesia_laser_otro='" & analgesia_laser_otro & "' ," & _
                   " analgesia_ultrasonido_otro='" & analgesia_ultrasonido_otro & "' ," & _
                   " analgesia_electroterapia_otro='" & analgesia_electroterapia_otro & "' ," & _
                   " idatendio='" & idterapista & "' ," & _
                   " FechaActualizacion=GETDATE()" & _
                   " where idcliente=" & idcliente & " and idcita =" & idcitafecha & "  "
                     
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = devolverDatospaciente(idcita, idcitafecha, turno)
                    
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                Else
                    'insert
                    'strSQL = " insert into [HMTratamientos] (idcliente,idcita,ultrasonido, chc, electroterapia, laser, cf, ejercicio, masaje, magneto, gimnasia, observaciones,idatendio,FechaActualizacion) " & _
                    '"VALUES(" & idcliente & "," & idcita & "," & ultrasonido & "," & chc & "," & electroterapia & "," & laser & "," & cf & "," & ejercicio & "," & masaje & "," & magneto & "," & gimnasia & ",'" & observaciones & "','" & idterapista & "',GETDATE()) "
                    strSQL = " insert into [HMTratamientos] (idcliente,idcita,exploracion_fisica,exploracion_analgesia, " & _
                         " exploracion_propiocepsion, exploracion_desinflamacion, exploracion_habilidades_manuales, exploracion_fortalecimiento, exploracion_aumentar_rangos, exploracion_reduccion_marcha, " & _
                         " exploracion_reintegracion_deportiva, analgesia_laser, analgesia_ultrasonido, analgesia_traccion_cervical, analgesia_electroterapia, analgesia_masaje, analgesia_traccion_lumbar, analgesia_tape, " & _
                         " analgesia_chc, analgesia_parafina, analgesia_magneto, analgesia_crio, analgesia_diatermia, analgesia_ondas, analgesia_hidroterapia, analgesia_terapia_manual, analgesia_banios_contraste, analgesia_cf, " & _
                         " analgesia_ejercicio, analgesia_gimnasia, estiramiento_sel,miembro_superior, columna, miembro_inferior, estiramiento_activo_sup, estiramiento_pasivo_sup, estiramiento_cintura_escapular, estiramiento_extensores_muneca, " & _
                         " estiramiento_extensores_codo, estiramiento_flexores_muneca, estiramiento_flexores_codo, estiramiento_maq_mov_pasiva_hombro, estiramiento_mano_hombro, estiramiento_superior_3_5, " & _
                         " estiramiento_superior_3_10, estiramiento_superior_3_15, estiramiento_dedos, estiramiento_otro_superior, estiramiento_otro_columna, estiramiento_otro_inferior, estiramiento_activo_columna, estiramiento_pasivo_columna, " & _
                         " estiramiento_paravertebrales_dorsales, estiramiento_paravertebrales_lumbares, estiramiento_cervicales, estiramiento_paravertebrales, estiramiento_columna_3_5, estiramiento_columna_3_10, " & _
                         " estiramiento_columna_3_15, estiramiento_trapecio, estiramiento_activo_inf, estiramiento_pasivo_inf, estiramiento_cuadriceps, estiramiento_tensor_fascia_lata, estiramiento_isquiotibiales, " & _
                         " estiramiento_tibial_anterior, estiramiento_aductores, estiramiento_tibial_posterior, estiramiento_abductores, estiramiento_peroneos, estiramiento_rotadores_cadera, estiramiento_maq_mov_pasiva_rodilla, " & _
                         " estiramiento_fascia_plantar, estiramiento_maq_mov_pasiva_tobillo, estiramiento_inferior_3_5, estiramiento_inferior_3_10, estiramiento_inferior_3_15, fortalecimiento_sel, fortalecimiento_cintura_escapular, " & _
                         " fortalecimiento_ligas, fortalecimiento_polainas, fortalecimiento_isometricas, fortalecimiento_sin_peso, fortalecimiento_extensores_muneca, fortalecimiento_extensores_codo, fortalecimiento_flexores_muneca, " & _
                         " fortalecimiento_flexores_codo, fortalecimiento_maq_mov_pasiva_hombro, fortalecimiento_mano_hombro, fortalecimiento_superior_3_5, fortalecimiento_superior_3_10, fortalecimiento_superior_3_15, " & _
                         " fortalecimiento_dedos, fortalecimiento_otro_superior,fortalecimiento_otro_columna,fortalecimiento_otro_inferior, fortalecimiento_paravertebrales_dorsales, fortalecimiento_williams, fortalecimiento_mckenzic, fortalecimiento_core, fortalecimiento_klapp, " & _
                         " fortalecimiento_paravertebrales_lumbares, fortalecimiento_cervicales, fortalecimiento_paravertebrales, fortalecimiento_columna_3_5, fortalecimiento_columna_3_10, fortalecimiento_columna_3_15, " & _
                         " fortalecimiento_trapecio, fortalecimiento_ligas_columna, fortalecimiento_isometricas_columna, fortalecimiento_cuadriceps, fortalecimiento_ligas_inf, fortalecimiento_isometricas_inf, " & _
                         " fortalecimiento_tensor_fascia_lata, fortalecimiento_isquiotibiales, fortalecimiento_tibial_anterior, fortalecimiento_aductores, fortalecimiento_tibial_posterior, fortalecimiento_abductores, " & _
                         " fortalecimiento_peroneos, fortalecimiento_rotadores_cadera, fortalecimiento_fascia_plantar, fortalecimiento_polainas_inf, fortalecimiento_sin_peso_inf, fortalecimiento_banco, fortalecimiento_trampolin, " & _
                         " fortalecimiento_bossu, fortalecimiento_inferior_3_5, fortalecimiento_inferior_3_10, fortalecimiento_inferior_3_15, reacondicionamiento_bici, reacondicionamiento_tiempo_bici, reacondicionamiento_caminadora, " & _
                         " reacondicionamiento_tiempo_caminadora, reacondicionamiento_reduccion_marcha, reacondicionamiento_eliptica, reacondicionamiento_escaleras, reacondicionamiento_pelotas, " & _
                         " observaciones, idatendio,analgesia_laser_otro,analgesia_ultrasonido_otro,analgesia_electroterapia_otro, FechaActualizacion) " & _
                         " VALUES (" & idcliente & ", " & _
      " " & idcitafecha & " , " & _
      " '" & exploracion_fisica & "' , " & _
      " " & exploracion_analgesia & " , " & _
      " " & exploracion_propiocepsion & " , " & _
      " " & exploracion_desinflamacion & " , " & _
      " " & exploracion_habilidades_manuales & " , " & _
      " " & exploracion_fortalecimiento & " , " & _
      " " & exploracion_aumentar_rangos & " , " & _
      " " & exploracion_reduccion_marcha & " , " & _
      " " & exploracion_reintegracion_deportiva & " , " & _
      " " & analgesia_laser & " , " & _
      " " & analgesia_ultrasonido & " , " & _
      " " & analgesia_traccion_cervical & " , " & _
      " " & analgesia_electroterapia & " , " & _
      " " & analgesia_masaje & " , " & _
      " " & analgesia_traccion_lumbar & " , " & _
      " " & analgesia_tape & " , " & _
      " " & analgesia_chc & " , " & _
      " " & analgesia_parafina & " , " & _
      " " & analgesia_magneto & " , " & _
      " " & analgesia_crio & " , " & _
      " " & analgesia_diatermia & " , " & _
      " " & analgesia_ondas & " , " & _
      " " & analgesia_hidroterapia & " , " & _
      " " & analgesia_terapia_manual & " , " & _
      " " & analgesia_banios_contraste & " , " & _
      " " & analgesia_cf & " , " & _
      " " & analgesia_ejercicio & " , " & _
      " " & analgesia_gimnasia & " , " & _
      " " & estiramiento_sel & " , " & _
      " " & miembro_superior & " , " & _
      " " & columna & " , " & _
      " " & miembro_inferior & " , " & _
      " " & estiramiento_activo_sup & " , " & _
      " " & estiramiento_pasivo_sup & " , " & _
      " " & estiramiento_cintura_escapular & " , " & _
      " " & estiramiento_extensores_muneca & " , " & _
      " " & estiramiento_extensores_codo & " , " & _
      " " & estiramiento_flexores_muneca & " , " & _
      " " & estiramiento_flexores_codo & " , " & _
      " " & estiramiento_maq_mov_pasiva_hombro & " , " & _
      " " & estiramiento_mano_hombro & " , " & _
      " " & estiramiento_superior_3_5 & " , " & _
      " " & estiramiento_superior_3_10 & " , " & _
      " " & estiramiento_superior_3_15 & " , " & _
      " " & estiramiento_dedos & " , " & _
      " '" & estiramiento_otro_superior & "' , " & _
      " '" & estiramiento_otro_columna & "' , " & _
      " '" & estiramiento_otro_inferior & "' , " & _
      " " & estiramiento_activo_columna & " , " & _
      " " & estiramiento_pasivo_columna & " , " & _
      " " & estiramiento_paravertebrales_dorsales & " , " & _
      " " & estiramiento_paravertebrales_lumbares & " , " & _
      " " & estiramiento_cervicales & " , " & _
      " " & estiramiento_paravertebrales & " , " & _
      " " & estiramiento_columna_3_5 & " , " & _
      " " & estiramiento_columna_3_10 & " , " & _
      " " & estiramiento_columna_3_15 & " , " & _
      " " & estiramiento_trapecio & " , " & _
      " " & estiramiento_activo_inf & " , " & _
      " " & estiramiento_pasivo_inf & " , " & _
      " " & estiramiento_cuadriceps & " , " & _
      " " & estiramiento_tensor_fascia_lata & " , " & _
      " " & estiramiento_isquiotibiales & " , " & _
      " " & estiramiento_tibial_anterior & " , " & _
      " " & estiramiento_aductores & " , " & _
      " " & estiramiento_tibial_posterior & " , " & _
      " " & estiramiento_abductores & " , " & _
      " " & estiramiento_peroneos & " , " & _
      " " & estiramiento_rotadores_cadera & " , " & _
      " " & estiramiento_maq_mov_pasiva_rodilla & " , " & _
      " " & estiramiento_fascia_plantar & " , " & _
      " " & estiramiento_maq_mov_pasiva_tobillo & " , " & _
      " " & estiramiento_inferior_3_5 & " , " & _
      " " & estiramiento_inferior_3_10 & " , " & _
      " " & estiramiento_inferior_3_15 & " , " & _
      " " & fortalecimiento_sel & " , " & _
      " " & fortalecimiento_cintura_escapular & " , " & _
      " " & fortalecimiento_ligas & " , " & _
      " " & fortalecimiento_polainas & " , " & _
      " " & fortalecimiento_isometricas & " , " & _
      " " & fortalecimiento_sin_peso & " , " & _
      " " & fortalecimiento_extensores_muneca & " , " & _
      " " & fortalecimiento_extensores_codo & " , " & _
      " " & fortalecimiento_flexores_muneca & " , " & _
      " " & fortalecimiento_flexores_codo & " , " & _
      " " & fortalecimiento_maq_mov_pasiva_hombro & " , " & _
      " " & fortalecimiento_mano_hombro & " , " & _
      " " & fortalecimiento_superior_3_5 & " , " & _
      " " & fortalecimiento_superior_3_10 & " , " & _
      " " & fortalecimiento_superior_3_15 & " , " & _
      " " & fortalecimiento_dedos & " , " & _
      " '" & fortalecimiento_otro_superior & "' , " & _
      " '" & fortalecimiento_otro_columna & "' , " & _
      " '" & fortalecimiento_otro_inferior & "' , " & _
      " " & fortalecimiento_paravertebrales_dorsales & " , " & _
      " " & fortalecimiento_williams & " , " & _
      " " & fortalecimiento_mckenzic & " , " & _
      " " & fortalecimiento_core & " , " & _
      " " & fortalecimiento_klapp & " , " & _
      " " & fortalecimiento_paravertebrales_lumbares & " , " & _
      " " & fortalecimiento_cervicales & " , " & _
      " " & fortalecimiento_paravertebrales & " , " & _
      " " & fortalecimiento_columna_3_5 & " , " & _
      " " & fortalecimiento_columna_3_10 & " , " & _
      " " & fortalecimiento_columna_3_15 & " , " & _
      " " & fortalecimiento_trapecio & " , " & _
      " " & fortalecimiento_ligas_columna & " , " & _
      " " & fortalecimiento_isometricas_columna & " , " & _
      " " & fortalecimiento_cuadriceps & " , " & _
      " " & fortalecimiento_ligas_inf & " , " & _
      " " & fortalecimiento_isometricas_inf & " , " & _
      " " & fortalecimiento_tensor_fascia_lata & " , " & _
      " " & fortalecimiento_isquiotibiales & " , " & _
      " " & fortalecimiento_tibial_anterior & " , " & _
      " " & fortalecimiento_aductores & " , " & _
      " " & fortalecimiento_tibial_posterior & " , " & _
      " " & fortalecimiento_abductores & " , " & _
      " " & fortalecimiento_peroneos & " , " & _
      " " & fortalecimiento_rotadores_cadera & " , " & _
      " " & fortalecimiento_fascia_plantar & " , " & _
      " " & fortalecimiento_polainas_inf & " , " & _
      " " & fortalecimiento_sin_peso_inf & " , " & _
      " " & fortalecimiento_banco & " , " & _
      " " & fortalecimiento_trampolin & " , " & _
      " " & fortalecimiento_bossu & " , " & _
      " " & fortalecimiento_inferior_3_5 & " , " & _
      " " & fortalecimiento_inferior_3_10 & " , " & _
      " " & fortalecimiento_inferior_3_15 & " , " & _
      " " & reacondicionamiento_bici & " , " & _
      " '" & reacondicionamiento_tiempo_bici & "' , " & _
      " " & reacondicionamiento_caminadora & " , " & _
      " '" & reacondicionamiento_tiempo_caminadora & "' , " & _
      " " & reacondicionamiento_reduccion_marcha & " , " & _
      " " & reacondicionamiento_eliptica & " , " & _
      " " & reacondicionamiento_escaleras & " , " & _
      " " & reacondicionamiento_pelotas & " , " & _
      " '" & observaciones & "', " & _
      " '" & idterapista & "', " & _
      " '" & analgesia_laser_otro & "', " & _
      " '" & analgesia_ultrasonido_otro & "', " & _
      " '" & analgesia_electroterapia_otro & "', " & _
      " GETDATE()) "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = devolverDatospaciente(idcita, idcitafecha, turno)
                    
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            
            
            
            
            data = "[]"
            
        ElseIf opcion = 3 Then
            
            'Leer datos del historial
            data = devolverDatoshistorial(idcitafecha, idcita, turno, idcliente)
            
            
        ElseIf opcion = 4 Then
            'Guardar el diagnostico y devolver todos los diagnosticos registrados
            
            strSQL = " select  IdHDiagnostico FROM [HMDiagnosticos]  where IdDiagnostico=" & id_diagnostico & " and idcita =" & idcita & " and Status='True'  "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'mensaje que ya existe
                    data = "[]"
                Else
                    'insert
                    strSQL = " insert into [HMDiagnosticos] (IdDiagnostico,IdCita,Idcliente, FechaActualizacion, Status) " & _
                    "VALUES(" & id_diagnostico & "," & idcita & "," & idcliente & ",GETDATE(),'True') "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        'mensaje de guardado correctamente
                        data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            data = devolverDatosdiagnosticos((idcita), (idcliente))
        
        ElseIf opcion = 5 Then
            'Eliminar el diagnostico y devolver todos los diagnosticos registrados
            
            strSQL = "    update HMDiagnosticos  set Status = 'False', FechaActualizacion = getdate() " & _
                 "where  IdHDiagnostico=" & idH_diagnostico & " and Status='True'"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                data = devolverDatosdiagnosticos((idcita), (idcliente))
            Else
                data = "{""resp"" : ""1""}"
            End If
            
            'data = "[{""id"":""1"",""diagnostico"":""hombro"",""fecha"":""20-03-2018""}]"
        ElseIf opcion = 6 Then
            'Devolver la lista de protocolos guardados en el historial
            
            'data = llenarprotocolos()
            data = devolverprotocolosguardados((idcita), (idcliente))
            ' data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""FASE 1"",""fecha"":""20-03-2018""}]"
           
        ElseIf opcion = 7 Then
            'Devolver la incormación del protocolo
            'id_protocolo
            
            
            data = devolverDatosprotocolos(id_protocolo)
           
        ElseIf opcion = 8 Then
            'Guardar el protocolo en el expediente médico del paciente
            'id_protocolo
            
            Dim strSqlp As String
            Dim dtp As New DataTable
            Dim Tratamientop As String
            Dim contraindicacionp As String
            Dim observacionesp As String
            Dim TratamientoM As String
            Dim contraindicacionM As String
            Dim observacionesM As String
       
            strSqlp = " select equipoejercicio,dosificacion,intensidad,tiempo,precauciones from protocolos where id='" + id_protocolo + "' "
        
        
            If clsDatos.cargatabla(strSqlp, dtp) = 0 Then
                If dtp.Rows.Count > 0 Then
              
                    Tratamientop = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Dosificación".PadRight(30, " ") + "|" + "Tiempo".PadRight(15, " ") + "|" + Chr(10)
                    Tratamientop = Tratamientop + "-".PadRight(105, "-") + Chr(10)
                    contraindicacionp = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Precauciones".PadRight(30, " ") + "|" + Chr(10)
                    contraindicacionp = contraindicacionp + "-".PadRight(63, "-") + Chr(10)
                    observacionesp = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Intensidad".PadRight(25, " ") + "|" + Chr(10)
                    observacionesp = observacionesp + "-".PadRight(58, "-") + Chr(10)

                  
                    For Each F As DataRow In dtp.Rows
                  
                        Tratamientop = Tratamientop + "|" + F.Item("equipoejercicio").ToString.PadRight(30, " ") + "|" + F.Item("dosificacion").ToString.PadRight(30, " ") + "|" + F.Item("tiempo").ToString.PadRight(15, " ") + "|" + Chr(10)
                        contraindicacionp = contraindicacionp + "|" + F.Item("equipoejercicio").ToString.PadRight(30, " ") + "|" + F.Item("precauciones").ToString.PadRight(30, " ") + "|" + Chr(10)
                        observacionesp = observacionesp + "|" + F.Item("equipoejercicio").ToString.PadRight(30, " ") + "|" + F.Item("intensidad").ToString.PadRight(25, " ") + "|" + Chr(10)
       
                    Next
                    
                    TratamientoM = Tratamientop
                    contraindicacionM = contraindicacionp
                    observacionesM = observacionesp

                End If
            Else
                data = "[]"
            End If
            
            strSQL = " select idHProtocolo FROM [HMProtocolos]  where IdProtocolo=" & id_protocolo & " and idcliente=" & idcliente & " and idcita =" & idcita & "  "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'mensaje que ya existe
                    data = "[]"
                Else
                    'insert
                    strSQL = " insert into [HMProtocolos] (idcliente, idcita, idProtocolo, tratamiento, contraindicaciones, observaciones,fase, Status,FechaActualizacion) " & _
                    "VALUES(" & idcliente & "," & idcita & "," & id_protocolo & ",'" & TratamientoM & "','" & contraindicacionM & "','" & observacionesM & "','1','True',GETDATE()) "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            
            
            data = devolverprotocolosguardados((idcita), (idcliente))
           
           
        
        ElseIf opcion = 9 Then
            'Eliminar protocolo de la tabla y devolver la tabla con la información
            
             
            strSQL = "    update HMProtocolos  set Status = 'False', FechaActualizacion = getdate() " & _
                 "where  IdHProtocolo=" & idH_protocolo & " and Status='True'"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                data = devolverprotocolosguardados((idcita), (idcliente))
            Else
                data = "[]"
            End If
            
            'data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""FASE 1"",""fecha"":""20-03-2018""}]"
            
            
            
        ElseIf opcion = 10 Then
            'El id del protocolo esta en la variable id
            
            
            strSQL = "    update HMProtocolos  set fase = " & fase & ", FechaActualizacion = getdate() " & _
                "where  IdHProtocolo=" & idH_protocolo & " and Status='True'"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                data = devolverprotocolosguardados((idcita), (idcliente))
            Else
                data = "[]"
            End If
            
            'id_protocolo = id
            'strSQL = " select idHProtocolo FROM [HMProtocolos]  where IdProtocolo=" & id_protocolo & " and idcliente=" & idcliente & " and idcita =" & idcita & "  "
            'If clsDatos.cargatabla(strSQL, dt) = 0 Then
            '    If dt.Rows.Count > 0 Then
            '        strSQL = " update HMProtocolos  set Fase = " & fase & ", FechaActualizacion = getdate() " & _
            '             "where  IdHProtocolo=" & idH_protocolo & " and Status='True'"
            '        clsDatos.cargaComando(strSQL)
            '        If clsDatos.ejecutar() = 0 Then
            '            data = devolverprotocolosguardados((idcita), (idcliente))
            '        Else
            '            data = "[]"
            '        End If
            '    Else
                  
            '    End If
            'End If
            'data = devolverprotocolosguardados((idcita), (idcliente))
            
    
            'Cambiar la fase del protocolo con la fecha actual y devolver la tabla con la información
            'data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""FASE 1"",""fecha"":""20-03-2018""}]"
        ElseIf opcion = 11 Then
            'La opción 11 no devuelve nada solo abre el modal en la vista del usuario para agregar el diagnostico nuevo
            data = "[]"
        ElseIf opcion = 12 Then
            'Se guarda el diagnosito nuevo en la tabla
            
            
            strSQL = " insert into [" & clsDatos.BaseDatos & "].[dbo].[CatDiagnosticos] (Descripcion, Status) VALUES('" & nombre_diagnostico & "','True')"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                data = devolverDatosdiagnosticos((idcita), (idcliente))
            Else
                data = "[]"
            End If
            
            'data = "[{""id"":""1"", ""nombre"":""Nuevo""}]"
        ElseIf opcion = 13 Then
            data = devolverDatosproximascitas((idcita))
            
            'data = "[{""fecha"":""20-03-2018"", ""atiende"":""Rosa Celeste""},{""fecha"":""20-03-2019"", ""atiende"":""Alma María Rico""}]"
        End If
        context.Response.ContentType = "text/plain"
        context.Response.Write(data)
    End Sub
    
    Function devolverDatospaciente(ByRef idcita As String, ByRef idcitafecha As String, ByRef turno As String) As String
        Dim strSql As String
        Dim strSql2 As String
        Dim strSql3 As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        Dim Fechas As String
        Dim icita As String
        Dim CTerapista As String
        Dim CidTerapista As String
  
        strSql = " select P.idcliente as Id,(P.paterno+' '+P.materno+' '+P.nombre) as paciente,P.edad as edad,p.domicilio as direccion, p.ciudadorigen as origen, CO.descripcion as ocupacion,p.fechanacimiento as fechanacimiento, " & _
        "T.IMedicas as IMedicas, T.CIndicaciones as CIndicaciones,AM.AlegiasPadecimientos as AlegiasPadecimientos, " & _
        "A.Fecha as FechaAgenda, " & _
      "isnull(HT.exploracion_fisica,'') as exploracion_fisica ," & _
      "isnull(HT.exploracion_analgesia,0) as exploracion_analgesia ," & _
      "isnull(HT.exploracion_propiocepsion,0) as exploracion_propiocepsion ," & _
      "isnull(HT.exploracion_desinflamacion,0) as exploracion_desinflamacion ," & _
      "isnull(HT.exploracion_habilidades_manuales,0) as exploracion_habilidades_manuales ," & _
      "isnull(HT.exploracion_fortalecimiento,0) as exploracion_fortalecimiento ," & _
      "isnull(HT.exploracion_aumentar_rangos,0) as exploracion_aumentar_rangos ," & _
      "isnull(HT.exploracion_reduccion_marcha,0) as exploracion_reduccion_marcha ," & _
      "isnull(HT.exploracion_reintegracion_deportiva,0) as exploracion_reintegracion_deportiva ," & _
      "isnull(HT.analgesia_laser,0) as analgesia_laser ," & _
      "isnull(HT.analgesia_ultrasonido,0) as analgesia_ultrasonido ," & _
      "isnull(HT.analgesia_traccion_cervical,0) as analgesia_traccion_cervical ," & _
      "isnull(HT.analgesia_electroterapia,0) as analgesia_electroterapia ," & _
      "isnull(HT.analgesia_masaje,0) as analgesia_masaje ," & _
      "isnull(HT.analgesia_traccion_lumbar,0) as analgesia_traccion_lumbar ," & _
      "isnull(HT.analgesia_tape,0) as analgesia_tape ," & _
      "isnull(HT.analgesia_chc,0) as analgesia_chc ," & _
      "isnull(HT.analgesia_parafina,0) as analgesia_parafina ," & _
      "isnull(HT.analgesia_magneto,0) as analgesia_magneto ," & _
      "isnull(HT.analgesia_crio,0) as analgesia_crio ," & _
      "isnull(HT.analgesia_diatermia,0) as analgesia_diatermia ," & _
      "isnull(HT.analgesia_ondas,0) as analgesia_ondas ," & _
      "isnull(HT.analgesia_hidroterapia,0) as analgesia_hidroterapia ," & _
      "isnull(HT.analgesia_terapia_manual,0) as analgesia_terapia_manual ," & _
      "isnull(HT.analgesia_banios_contraste,0) as analgesia_banios_contraste ," & _
      "isnull(HT.analgesia_cf,0) as analgesia_cf ," & _
      "isnull(HT.analgesia_ejercicio,0) as analgesia_ejercicio ," & _
      "isnull(HT.analgesia_gimnasia,0) as analgesia_gimnasia ," & _
      "isnull(HT.estiramiento_sel,0) as estiramiento_sel ," & _
      "isnull(HT.miembro_superior,0) as miembro_superior ," & _
      "isnull(HT.columna,0) as columna ," & _
      "isnull(HT.miembro_inferior,0) as miembro_inferior ," & _
      "isnull(HT.estiramiento_activo_sup,0) as estiramiento_activo_sup ," & _
      "isnull(HT.estiramiento_pasivo_sup,0) as estiramiento_pasivo_sup ," & _
      "isnull(HT.estiramiento_cintura_escapular,0) as estiramiento_cintura_escapular ," & _
      "isnull(HT.estiramiento_extensores_muneca,0) as estiramiento_extensores_muneca ," & _
      "isnull(HT.estiramiento_extensores_codo,0) as estiramiento_extensores_codo ," & _
      "isnull(HT.estiramiento_flexores_muneca,0) as estiramiento_flexores_muneca ," & _
      "isnull(HT.estiramiento_flexores_codo,0) as estiramiento_flexores_codo ," & _
      "isnull(HT.estiramiento_maq_mov_pasiva_hombro,0) as estiramiento_maq_mov_pasiva_hombro ," & _
      "isnull(HT.estiramiento_mano_hombro,0) as estiramiento_mano_hombro ," & _
      "isnull(HT.estiramiento_superior_3_5,0) as estiramiento_superior_3_5 ," & _
      "isnull(HT.estiramiento_superior_3_10,0) as estiramiento_superior_3_10 ," & _
      "isnull(HT.estiramiento_superior_3_15,0) as estiramiento_superior_3_15 ," & _
      "isnull(HT.estiramiento_dedos,0) as estiramiento_dedos ," & _
      "isnull(HT.estiramiento_otro_superior,'') as estiramiento_otro_superior ," & _
      "isnull(HT.estiramiento_otro_columna,'') as estiramiento_otro_columna ," & _
      "isnull(HT.estiramiento_otro_inferior,'') as estiramiento_otro_inferior ," & _
      "isnull(HT.estiramiento_activo_columna,0) as estiramiento_activo_columna ," & _
      "isnull(HT.estiramiento_pasivo_columna,0) as estiramiento_pasivo_columna ," & _
      "isnull(HT.estiramiento_paravertebrales_dorsales,0) as estiramiento_paravertebrales_dorsales ," & _
      "isnull(HT.estiramiento_paravertebrales_lumbares,0) as estiramiento_paravertebrales_lumbares ," & _
      "isnull(HT.estiramiento_cervicales,0) as estiramiento_cervicales ," & _
      "isnull(HT.estiramiento_paravertebrales,0) as estiramiento_paravertebrales ," & _
      "isnull(HT.estiramiento_columna_3_5,0) as estiramiento_columna_3_5 ," & _
      "isnull(HT.estiramiento_columna_3_10,0) as estiramiento_columna_3_10 ," & _
      "isnull(HT.estiramiento_columna_3_15,0) as estiramiento_columna_3_15 ," & _
      "isnull(HT.estiramiento_trapecio,0) as estiramiento_trapecio ," & _
      "isnull(HT.estiramiento_activo_inf,0) as estiramiento_activo_inf ," & _
      "isnull(HT.estiramiento_pasivo_inf,0) as estiramiento_pasivo_inf ," & _
      "isnull(HT.estiramiento_cuadriceps,0) as estiramiento_cuadriceps ," & _
      "isnull(HT.estiramiento_tensor_fascia_lata,0) as estiramiento_tensor_fascia_lata ," & _
      "isnull(HT.estiramiento_isquiotibiales,0) as estiramiento_isquiotibiales ," & _
      "isnull(HT.estiramiento_tibial_anterior,0) as estiramiento_tibial_anterior ," & _
      "isnull(HT.estiramiento_aductores,0) as estiramiento_aductores ," & _
      "isnull(HT.estiramiento_tibial_posterior,0) as estiramiento_tibial_posterior ," & _
      "isnull(HT.estiramiento_abductores,0) as estiramiento_abductores ," & _
      "isnull(HT.estiramiento_peroneos,0) as estiramiento_peroneos ," & _
      "isnull(HT.estiramiento_rotadores_cadera,0) as estiramiento_rotadores_cadera ," & _
      "isnull(HT.estiramiento_maq_mov_pasiva_rodilla,0) as estiramiento_maq_mov_pasiva_rodilla ," & _
      "isnull(HT.estiramiento_fascia_plantar,0) as estiramiento_fascia_plantar ," & _
      "isnull(HT.estiramiento_maq_mov_pasiva_tobillo,0) as estiramiento_maq_mov_pasiva_tobillo ," & _
      "isnull(HT.estiramiento_inferior_3_5,0) as estiramiento_inferior_3_5 ," & _
      "isnull(HT.estiramiento_inferior_3_10,0) as estiramiento_inferior_3_10 ," & _
      "isnull(HT.estiramiento_inferior_3_15,0) as estiramiento_inferior_3_15 ," & _
      "isnull(HT.fortalecimiento_sel,0) as fortalecimiento_sel ," & _
      "isnull(HT.fortalecimiento_cintura_escapular,0) as fortalecimiento_cintura_escapular ," & _
      "isnull(HT.fortalecimiento_ligas,0) as fortalecimiento_ligas ," & _
      "isnull(HT.fortalecimiento_polainas,0) as fortalecimiento_polainas ," & _
      "isnull(HT.fortalecimiento_isometricas,0) as fortalecimiento_isometricas ," & _
      "isnull(HT.fortalecimiento_sin_peso,0) as fortalecimiento_sin_peso ," & _
      "isnull(HT.fortalecimiento_extensores_muneca,0) as fortalecimiento_extensores_muneca ," & _
      "isnull(HT.fortalecimiento_extensores_codo,0) as fortalecimiento_extensores_codo ," & _
      "isnull(HT.fortalecimiento_flexores_muneca,0) as fortalecimiento_flexores_muneca ," & _
      "isnull(HT.fortalecimiento_flexores_codo,0) as fortalecimiento_flexores_codo ," & _
      "isnull(HT.fortalecimiento_maq_mov_pasiva_hombro,0) as fortalecimiento_maq_mov_pasiva_hombro ," & _
      "isnull(HT.fortalecimiento_mano_hombro,0) as fortalecimiento_mano_hombro ," & _
      "isnull(HT.fortalecimiento_superior_3_5,0) as fortalecimiento_superior_3_5 ," & _
      "isnull(HT.fortalecimiento_superior_3_10,0) as fortalecimiento_superior_3_10 ," & _
      "isnull(HT.fortalecimiento_superior_3_15,0) as fortalecimiento_superior_3_15 ," & _
      "isnull(HT.fortalecimiento_dedos,0) as fortalecimiento_dedos ," & _
      "isnull(HT.fortalecimiento_otro_superior,'') as fortalecimiento_otro_superior ," & _
      "isnull(HT.fortalecimiento_otro_columna,'') as fortalecimiento_otro_columna ," & _
      "isnull(HT.fortalecimiento_otro_inferior,'') as fortalecimiento_otro_inferior ," & _
      "isnull(HT.fortalecimiento_paravertebrales_dorsales,0) as fortalecimiento_paravertebrales_dorsales ," & _
      "isnull(HT.fortalecimiento_williams,0) as fortalecimiento_williams ," & _
      "isnull(HT.fortalecimiento_mckenzic,0) as fortalecimiento_mckenzic ," & _
      "isnull(HT.fortalecimiento_core,0) as fortalecimiento_core ," & _
      "isnull(HT.fortalecimiento_klapp,0) as fortalecimiento_klapp ," & _
      "isnull(HT.fortalecimiento_paravertebrales_lumbares,0) as fortalecimiento_paravertebrales_lumbares ," & _
      "isnull(HT.fortalecimiento_cervicales,0) as fortalecimiento_cervicales ," & _
      "isnull(HT.fortalecimiento_paravertebrales,0) as fortalecimiento_paravertebrales ," & _
      "isnull(HT.fortalecimiento_columna_3_5,0) as fortalecimiento_columna_3_5 ," & _
      "isnull(HT.fortalecimiento_columna_3_10,0) as fortalecimiento_columna_3_10 ," & _
      "isnull(HT.fortalecimiento_columna_3_15,0) as fortalecimiento_columna_3_15 ," & _
      "isnull(HT.fortalecimiento_trapecio,0) as fortalecimiento_trapecio ," & _
      "isnull(HT.fortalecimiento_ligas_columna,0) as fortalecimiento_ligas_columna ," & _
      "isnull(HT.fortalecimiento_isometricas_columna,0) as fortalecimiento_isometricas_columna ," & _
      "isnull(HT.fortalecimiento_cuadriceps,0) as fortalecimiento_cuadriceps ," & _
      "isnull(HT.fortalecimiento_ligas_inf,0) as fortalecimiento_ligas_inf ," & _
      "isnull(HT.fortalecimiento_isometricas_inf,0) as fortalecimiento_isometricas_inf ," & _
      "isnull(HT.fortalecimiento_tensor_fascia_lata,0) as fortalecimiento_tensor_fascia_lata ," & _
      "isnull(HT.fortalecimiento_isquiotibiales,0) as fortalecimiento_isquiotibiales ," & _
      "isnull(HT.fortalecimiento_tibial_anterior,0) as fortalecimiento_tibial_anterior ," & _
      "isnull(HT.fortalecimiento_aductores,0) as fortalecimiento_aductores ," & _
      "isnull(HT.fortalecimiento_tibial_posterior,0) as fortalecimiento_tibial_posterior ," & _
      "isnull(HT.fortalecimiento_abductores,0) as fortalecimiento_abductores ," & _
      "isnull(HT.fortalecimiento_peroneos,0) as fortalecimiento_peroneos ," & _
      "isnull(HT.fortalecimiento_rotadores_cadera,0) as fortalecimiento_rotadores_cadera ," & _
      "isnull(HT.fortalecimiento_fascia_plantar,0) as fortalecimiento_fascia_plantar ," & _
      "isnull(HT.fortalecimiento_polainas_inf,0) as fortalecimiento_polainas_inf ," & _
      "isnull(HT.fortalecimiento_sin_peso_inf,0) as fortalecimiento_sin_peso_inf ," & _
      "isnull(HT.fortalecimiento_banco,0) as fortalecimiento_banco ," & _
      "isnull(HT.fortalecimiento_trampolin,0) as fortalecimiento_trampolin ," & _
      "isnull(HT.fortalecimiento_bossu,0) as fortalecimiento_bossu ," & _
      "isnull(HT.fortalecimiento_inferior_3_5,0) as fortalecimiento_inferior_3_5 ," & _
      "isnull(HT.fortalecimiento_inferior_3_10,0) as fortalecimiento_inferior_3_10 ," & _
      "isnull(HT.fortalecimiento_inferior_3_15,0) as fortalecimiento_inferior_3_15 ," & _
      "isnull(HT.reacondicionamiento_bici,0) as reacondicionamiento_bici ," & _
      "isnull(HT.reacondicionamiento_tiempo_bici,0) as reacondicionamiento_tiempo_bici ," & _
      "isnull(HT.reacondicionamiento_caminadora,0) as reacondicionamiento_caminadora ," & _
      "isnull(HT.reacondicionamiento_tiempo_caminadora,0) as reacondicionamiento_tiempo_caminadora ," & _
      "isnull(HT.reacondicionamiento_reduccion_marcha,0) as reacondicionamiento_reduccion_marcha ," & _
      "isnull(HT.reacondicionamiento_eliptica,0) as reacondicionamiento_eliptica ," & _
      "isnull(HT.reacondicionamiento_escaleras,0) as reacondicionamiento_escaleras ," & _
      "isnull(HT.reacondicionamiento_pelotas,0) as reacondicionamiento_pelotas ," & _
      "isnull(HT.observaciones,'') as observaciones ," & _
      "isnull(HT.analgesia_laser_otro,'') as analgesia_laser_otro ," & _
      "isnull(HT.analgesia_ultrasonido_otro,'') as analgesia_ultrasonido_otro ," & _
      "isnull(HT.analgesia_electroterapia_otro,'') as analgesia_electroterapia_otro ," & _
      "isnull(TP.Nombre,'') as atendio " & _
        "from agenda A " & _
        "inner join clientes P on P.idcliente=A.idcliente " & _
        "left join terapia T on T.idcliente=A.idcliente " & _
        "left join HMTratamientos HT on HT.idcita=A.idcita " & _
        "left join Terapistas TP on TP.id=HT.idatendio " & _
        "left join CatOcupaciones CO on CO.IdOcupacion=P.idocupacion " & _
        "left join HMAntecedentesMedicos AM on AM.idCliente=A.idCliente " & _
        "where A.idCita='" + idcita + "' "
        
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                
                strSql2 = "SELECT A.idcita as idcita,format(A.fecha,'dd/MM/yyyy') as fecha  FROM agenda A " & _
                " where A.idcliente = '" & dt.Rows(0).Item("Id") & "' AND estado<>'CANCELADO' and convert(varchar(10),((select fecha from agenda where Idcita = '" + idcita + "')),103)>=A.fecha " & _
                " order by A.fecha desc "
                If clsDatos.cargatabla(strSql2, dt2) = 0 Then
                    If dt.Rows.Count > 0 Then
                        Dim FechaA As String
                        Dim IdcitaA As String
                        Dim count2 As Integer = 1
                        For Each F As DataRow In dt2.Rows
                            If count2 > 1 Then
                                FechaA += "."
                                IdcitaA += "."
                            End If
                            count2 = count2 + 1
                            If Not IsDBNull(F.Item("fecha")) Then
                                FechaA += F.Item("fecha")
                            End If
                            If Not IsDBNull(F.Item("idcita")) Then
                                IdcitaA += F.Item("idcita").ToString
                            End If
                            
                            
                            
                        Next
                        Fechas = FechaA
                        icita = IdcitaA
                    End If
                Else
                    'FechaA
                End If
                
                
                strSql3 = "SELECT id, Nombre  FROM Terapistas T where turno='" + turno + "' " & _
              " order by id Asc "
                If clsDatos.cargatabla(strSql3, dt3) = 0 Then
                    If dt.Rows.Count > 0 Then
                        Dim Terapista As String
                        Dim IdTerapista As String
                        Dim countT As Integer = 1
                        For Each T As DataRow In dt3.Rows
                            If countT > 1 Then
                                Terapista += "."
                                IdTerapista += "."
                            End If
                            countT = countT + 1
                            If Not IsDBNull(T.Item("Nombre")) Then
                                Terapista += T.Item("Nombre")
                            End If
                            If Not IsDBNull(T.Item("id")) Then
                                IdTerapista += T.Item("id").ToString
                            End If
                            
                            
                            
                        Next
                        CTerapista = Terapista
                        CidTerapista = IdTerapista
                    End If
                Else
                    'TERAPISTA
                End If
                
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    'cadena += "{""id"":""" & i.Item("Id") & """,""nombre"":""" & i.Item("paciente") & """,""fecha_nac"":""" & i.Item("fechanacimiento") & """,""edad"":""" & i.Item("edad") & """,""origen"":""" & i.Item("origen") & """,""ocupacion"":""" & i.Item("ocupacion") & """,""direccion"":""" & i.Item("direccion") & """,""alergias"":""" & i.Item("AlegiasPadecimientos") & """, ""indicaciones_medicas"":""" & i.Item("IMedicas") & """, ""contra_indicaciones"":""" & i.Item("CIndicaciones") & """ , ""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""ultrasonido"":""" & i.Item("ultrasonido") & """, ""chc"":""" & i.Item("chc") & """, ""electroterapia"":""" & i.Item("electroterapia") & """, ""laser"":""" & i.Item("laser") & """, ""cf"":""" & i.Item("cf") & """, ""ejercicio"":""" & i.Item("ejercicio") & """,""masaje"":""" & i.Item("masaje") & """,""magneto"":""" & i.Item("magneto") & """,""gimnasia"":""" & i.Item("gimnasia") & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """}"
                    cadena += "{""id"":""" & i.Item("Id") & """,""nombre"":""" & i.Item("paciente") & """,""fecha_nac"":""" & i.Item("fechanacimiento") & """,""edad"":""" & i.Item("edad") & """,""origen"":""" & i.Item("origen") & """,""ocupacion"":""" & i.Item("ocupacion") & """,""direccion"":""" & i.Item("direccion") & """,""alergias"":""" & i.Item("AlegiasPadecimientos") & """, ""indicaciones_medicas"":""" & i.Item("IMedicas") & """, ""contra_indicaciones"":""" & i.Item("CIndicaciones") & """ , ""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """,""exploracion_fisica"" : """ & i.Item("exploracion_fisica") & """,""exploracion_analgesia"" : """ & i.Item("exploracion_analgesia") & """,""exploracion_propiocepsion"" : """ & i.Item("exploracion_propiocepsion") & """,""exploracion_desinflamacion"" : """ & i.Item("exploracion_desinflamacion") & """,""exploracion_habilidades_manuales"" : """ & i.Item("exploracion_habilidades_manuales") & """,""exploracion_fortalecimiento"" : """ & i.Item("exploracion_fortalecimiento") & """,""exploracion_aumentar_rangos"" : """ & i.Item("exploracion_aumentar_rangos") & """,""exploracion_reduccion_marcha"" : """ & i.Item("exploracion_reduccion_marcha") & """,""exploracion_reintegracion_deportiva"" : """ & i.Item("exploracion_reintegracion_deportiva") & """,""analgesia_laser"" : """ & i.Item("analgesia_laser") & """,""analgesia_ultrasonido"" : """ & i.Item("analgesia_ultrasonido") & """,""analgesia_traccion_cervical"" : """ & i.Item("analgesia_traccion_cervical") & """,""analgesia_electroterapia"" : """ & i.Item("analgesia_electroterapia") & """,""analgesia_masaje"" : """ & i.Item("analgesia_masaje") & """,""analgesia_traccion_lumbar"" : """ & i.Item("analgesia_traccion_lumbar") & """,""analgesia_tape"" : """ & i.Item("analgesia_tape") & """,""analgesia_chc"" : """ & i.Item("analgesia_chc") & """,""analgesia_parafina"" : """ & i.Item("analgesia_parafina") & """,""analgesia_magneto"" : """ & i.Item("analgesia_magneto") & """,""analgesia_crio"" : """ & i.Item("analgesia_crio") & """,""analgesia_diatermia"" : """ & i.Item("analgesia_diatermia") & """,""analgesia_ondas"" : """ & i.Item("analgesia_ondas") & """,""analgesia_hidroterapia"" : """ & i.Item("analgesia_hidroterapia") & """,""analgesia_terapia_manual"" : """ & i.Item("analgesia_terapia_manual") & """,""analgesia_banios_contraste"" : """ & i.Item("analgesia_banios_contraste") & """,""analgesia_cf"" : """ & i.Item("analgesia_cf") & """,""analgesia_ejercicio"" : """ & i.Item("analgesia_ejercicio") & """,""analgesia_gimnasia"" : """ & i.Item("analgesia_gimnasia") & """,""estiramiento_sel"" : """ & i.Item("estiramiento_sel") & """,""miembro_superior"" : """ & i.Item("miembro_superior") & """,""columna"" : """ & i.Item("columna") & """,""miembro_inferior"" : """ & i.Item("miembro_inferior") & """,""estiramiento_activo_sup"" : """ & i.Item("estiramiento_activo_sup") & """,""estiramiento_pasivo_sup"" : """ & i.Item("estiramiento_pasivo_sup") & """,""estiramiento_cintura_escapular"" : """ & i.Item("estiramiento_cintura_escapular") & """,""estiramiento_extensores_muneca"" : """ & i.Item("estiramiento_extensores_muneca") & """,""estiramiento_extensores_codo"" : """ & i.Item("estiramiento_extensores_codo") & """,""estiramiento_flexores_muneca"" : """ & i.Item("estiramiento_flexores_muneca") & """,""estiramiento_flexores_codo"" : """ & i.Item("estiramiento_flexores_codo") & """,""estiramiento_maq_mov_pasiva_hombro"" : """ & i.Item("estiramiento_maq_mov_pasiva_hombro") & """,""estiramiento_mano_hombro"" : """ & i.Item("estiramiento_mano_hombro") & """,""estiramiento_superior_3_5"" : """ & i.Item("estiramiento_superior_3_5") & """,""estiramiento_superior_3_10"" : """ & i.Item("estiramiento_superior_3_10") & """,""estiramiento_superior_3_15"" : """ & i.Item("estiramiento_superior_3_15") & """,""estiramiento_dedos"" : """ & i.Item("estiramiento_dedos") & """,""estiramiento_otro_superior"" : """ & i.Item("estiramiento_otro_superior") & """,""estiramiento_otro_columna"" : """ & i.Item("estiramiento_otro_columna") & """,""estiramiento_otro_inferior"" : """ & i.Item("estiramiento_otro_inferior") & """,""estiramiento_activo_columna"" : """ & i.Item("estiramiento_activo_columna") & """,""estiramiento_pasivo_columna"" : """ & i.Item("estiramiento_pasivo_columna") & """,""estiramiento_paravertebrales_dorsales"" : """ & i.Item("estiramiento_paravertebrales_dorsales") & """,""estiramiento_paravertebrales_lumbares"" : """ & i.Item("estiramiento_paravertebrales_lumbares") & """,""estiramiento_cervicales"" : """ & i.Item("estiramiento_cervicales") & """,""estiramiento_paravertebrales"" : """ & i.Item("estiramiento_paravertebrales") & """,""estiramiento_columna_3_5"" : """ & i.Item("estiramiento_columna_3_5") & """,""estiramiento_columna_3_10"" : """ & i.Item("estiramiento_columna_3_10") & """,""estiramiento_columna_3_15"" : """ & i.Item("estiramiento_columna_3_15") & """,""estiramiento_trapecio"" : """ & i.Item("estiramiento_trapecio") & """,""estiramiento_activo_inf"" : """ & i.Item("estiramiento_activo_inf") & """,""estiramiento_pasivo_inf"" : """ & i.Item("estiramiento_pasivo_inf") & """,""estiramiento_cuadriceps"" : """ & i.Item("estiramiento_cuadriceps") & """,""estiramiento_tensor_fascia_lata"" : """ & i.Item("estiramiento_tensor_fascia_lata") & """,""estiramiento_isquiotibiales"" : """ & i.Item("estiramiento_isquiotibiales") & """,""estiramiento_tibial_anterior"" : """ & i.Item("estiramiento_tibial_anterior") & """,""estiramiento_aductores"" : """ & i.Item("estiramiento_aductores") & """,""estiramiento_tibial_posterior"" : """ & i.Item("estiramiento_tibial_posterior") & """,""estiramiento_abductores"" : """ & i.Item("estiramiento_abductores") & """,""estiramiento_peroneos"" : """ & i.Item("estiramiento_peroneos") & """,""estiramiento_rotadores_cadera"" : """ & i.Item("estiramiento_rotadores_cadera") & """,""estiramiento_maq_mov_pasiva_rodilla"" : """ & i.Item("estiramiento_maq_mov_pasiva_rodilla") & """,""estiramiento_fascia_plantar"" : """ & i.Item("estiramiento_fascia_plantar") & """,""estiramiento_maq_mov_pasiva_tobillo"" : """ & i.Item("estiramiento_maq_mov_pasiva_tobillo") & """,""estiramiento_inferior_3_5"" : """ & i.Item("estiramiento_inferior_3_5") & """,""estiramiento_inferior_3_10"" : """ & i.Item("estiramiento_inferior_3_10") & """,""estiramiento_inferior_3_15"" : """ & i.Item("estiramiento_inferior_3_15") & """,""fortalecimiento_sel"" : """ & i.Item("fortalecimiento_sel") & """,""fortalecimiento_cintura_escapular"" : """ & i.Item("fortalecimiento_cintura_escapular") & """,""fortalecimiento_ligas"" : """ & i.Item("fortalecimiento_ligas") & """,""fortalecimiento_polainas"" : """ & i.Item("fortalecimiento_polainas") & """,""fortalecimiento_isometricas"" : """ & i.Item("fortalecimiento_isometricas") & """,""fortalecimiento_sin_peso"" : """ & i.Item("fortalecimiento_sin_peso") & """,""fortalecimiento_extensores_muneca"" : """ & i.Item("fortalecimiento_extensores_muneca") & """,""fortalecimiento_extensores_codo"" : """ & i.Item("fortalecimiento_extensores_codo") & """,""fortalecimiento_flexores_muneca"" : """ & i.Item("fortalecimiento_flexores_muneca") & """,""fortalecimiento_flexores_codo"" : """ & i.Item("fortalecimiento_flexores_codo") & """,""fortalecimiento_maq_mov_pasiva_hombro"" : """ & i.Item("fortalecimiento_maq_mov_pasiva_hombro") & """,""fortalecimiento_mano_hombro"" : """ & i.Item("fortalecimiento_mano_hombro") & """,""fortalecimiento_superior_3_5"" : """ & i.Item("fortalecimiento_superior_3_5") & """,""fortalecimiento_superior_3_10"" : """ & i.Item("fortalecimiento_superior_3_10") & """,""fortalecimiento_superior_3_15"" : """ & i.Item("fortalecimiento_superior_3_15") & """,""fortalecimiento_dedos"" : """ & i.Item("fortalecimiento_dedos") & """,""fortalecimiento_otro_superior"" : """ & i.Item("fortalecimiento_otro_superior") & """,""fortalecimiento_otro_columna"" : """ & i.Item("fortalecimiento_otro_columna") & """,""fortalecimiento_otro_inferior"" : """ & i.Item("fortalecimiento_otro_inferior") & """,""fortalecimiento_paravertebrales_dorsales"" : """ & i.Item("fortalecimiento_paravertebrales_dorsales") & """,""fortalecimiento_williams"" : """ & i.Item("fortalecimiento_williams") & """,""fortalecimiento_mckenzic"" : """ & i.Item("fortalecimiento_mckenzic") & """,""fortalecimiento_core"" : """ & i.Item("fortalecimiento_core") & """,""fortalecimiento_klapp"" : """ & i.Item("fortalecimiento_klapp") & """,""fortalecimiento_paravertebrales_lumbares"" : """ & i.Item("fortalecimiento_paravertebrales_lumbares") & """,""fortalecimiento_cervicales"" : """ & i.Item("fortalecimiento_cervicales") & """,""fortalecimiento_paravertebrales"" : """ & i.Item("fortalecimiento_paravertebrales") & """,""fortalecimiento_columna_3_5"" : """ & i.Item("fortalecimiento_columna_3_5") & """,""fortalecimiento_columna_3_10"" : """ & i.Item("fortalecimiento_columna_3_10") & """,""fortalecimiento_columna_3_15"" : """ & i.Item("fortalecimiento_columna_3_15") & """,""fortalecimiento_trapecio"" : """ & i.Item("fortalecimiento_trapecio") & """,""fortalecimiento_ligas_columna"" : """ & i.Item("fortalecimiento_ligas_columna") & """,""fortalecimiento_isometricas_columna"" : """ & i.Item("fortalecimiento_isometricas_columna") & """,""fortalecimiento_cuadriceps"" : """ & i.Item("fortalecimiento_cuadriceps") & """,""fortalecimiento_ligas_inf"" : """ & i.Item("fortalecimiento_ligas_inf") & """,""fortalecimiento_isometricas_inf"" : """ & i.Item("fortalecimiento_isometricas_inf") & """,""fortalecimiento_tensor_fascia_lata"" : """ & i.Item("fortalecimiento_tensor_fascia_lata") & """,""fortalecimiento_isquiotibiales"" : """ & i.Item("fortalecimiento_isquiotibiales") & """,""fortalecimiento_tibial_anterior"" : """ & i.Item("fortalecimiento_tibial_anterior") & """,""fortalecimiento_aductores"" : """ & i.Item("fortalecimiento_aductores") & """,""fortalecimiento_tibial_posterior"" : """ & i.Item("fortalecimiento_tibial_posterior") & """,""fortalecimiento_abductores"" : """ & i.Item("fortalecimiento_abductores") & """,""fortalecimiento_peroneos"" : """ & i.Item("fortalecimiento_peroneos") & """,""fortalecimiento_rotadores_cadera"" : """ & i.Item("fortalecimiento_rotadores_cadera") & """,""fortalecimiento_fascia_plantar"" : """ & i.Item("fortalecimiento_fascia_plantar") & """,""fortalecimiento_polainas_inf"" : """ & i.Item("fortalecimiento_polainas_inf") & """,""fortalecimiento_sin_peso_inf"" : """ & i.Item("fortalecimiento_sin_peso_inf") & """,""fortalecimiento_banco"" : """ & i.Item("fortalecimiento_banco") & """,""fortalecimiento_trampolin"" : """ & i.Item("fortalecimiento_trampolin") & """,""fortalecimiento_bossu"" : """ & i.Item("fortalecimiento_bossu") & """,""fortalecimiento_inferior_3_5"" : """ & i.Item("fortalecimiento_inferior_3_5") & """,""fortalecimiento_inferior_3_10"" : """ & i.Item("fortalecimiento_inferior_3_10") & """,""fortalecimiento_inferior_3_15"" : """ & i.Item("fortalecimiento_inferior_3_15") & """,""reacondicionamiento_bici"" : """ & i.Item("reacondicionamiento_bici") & """,""reacondicionamiento_tiempo_bici"" : """ & i.Item("reacondicionamiento_tiempo_bici") & """,""reacondicionamiento_caminadora"" : """ & i.Item("reacondicionamiento_caminadora") & """,""reacondicionamiento_tiempo_caminadora"" : """ & i.Item("reacondicionamiento_tiempo_caminadora") & """,""reacondicionamiento_reduccion_marcha"" : """ & i.Item("reacondicionamiento_reduccion_marcha") & """,""reacondicionamiento_eliptica"" : """ & i.Item("reacondicionamiento_eliptica") & """,""reacondicionamiento_escaleras"" : """ & i.Item("reacondicionamiento_escaleras") & """,""reacondicionamiento_pelotas"" : """ & i.Item("reacondicionamiento_pelotas") & """,""analgesia_laser_otro"" : """ & i.Item("analgesia_laser_otro") & """,""analgesia_ultrasonido_otro"" : """ & i.Item("analgesia_ultrasonido_otro") & """,""analgesia_electroterapia_otro"" : """ & i.Item("analgesia_electroterapia_otro") & """}"
                    
                Next
                cadena += "]"
                Data = cadena

            End If
        Else
            'data = "[{""id"":""1"",""nombre"":""Christhian Froilan Sosa Cebalos"",""fecha_nac"":""24/04/1981"",""edad"":""37"",""origen"":""Mérida"",""ocupacion"":""Máster de la web"",""direccion"":""Vergel III"",""alergias"":""a los hombres"", ""indicaciones_medicas"":""Tomar viagra"", ""contra_indicaciones"":""No tomar viagra si tomó alcohol"",""id_diagnosticos"":""1.2.3"", ""diagnosticos"":""HOMBRO CONDROMATOSIS.HOMBRO.TOBILLO"", ""protocolo"":""3"",""fase"":""4"", ""fecha"":""20/02/2019.24/02/2019.28/02/2019"",""ultrasonido"":""1"", ""chc"":""1"", ""electroterapia"":""0"", ""laser"":""1"", ""cf"":""1"", ""ejercicio"":""1"",""masaje"":""0"",""magneto"":""0"",""gimnasia"":""0"",""observaciones"":""Esta muy guapo el paciente"" }]"
        End If
        Return Data
    End Function
    
    Function devolverDatosproximascitas(ByRef idcita As String) As String
        Dim strSql As String
        'Dim strSql2 As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        ' Dim Fechas As String
        'Dim icita As String
  
        strSql = " SELECT A.idcita as idcita,format(A.fecha,'dd/MM/yyyy') as fecha, TP.Nombre as nombre  FROM " & _
        "agenda A " & _
        "inner join clientes P on P.idcliente=A.idcliente " & _
        "left join Terapistas TP on TP.id=A.posicion " & _
        "where A.idcliente = (select idCliente from agenda where Idcita = '" + idcita + "') AND estado<>'CANCELADO' and convert(varchar(10),((select fecha from agenda where Idcita = '" + idcita + "')),103)<A.fecha  " & _
        "order by A.fecha desc "
       
   
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    'Data = "[{""fecha"":""20-03-2018"", ""atiende"":""Rosa Celeste""},{""fecha"":""20-03-2019"", ""atiende"":""Alma María Rico""}]"
                    
                    cadena += "{""fecha"":""" & i.Item("fecha") & """, ""atiende"":""" & i.Item("nombre") & """}"
                Next
                cadena += "]"
                Data = cadena
            Else
                Data = "{""resp"" : ""1""}"
            End If
        Else
            Data = "{""resp"" : ""1""}"
        
        End If
        Return Data
    End Function
    
    Function devolverDatoshistorial(ByRef idcitafecha As String, ByRef idcita As String, ByRef turno As String, ByRef idcliente As String) As String
       
        Dim strSql As String
        Dim strSql2 As String
        Dim strSql3 As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        Dim Fechas As String
        Dim icita As String
        Dim CTerapista As String
        Dim CidTerapista As String
        
        '  strSql = " SELECT idtratamiento, idcliente, idcita, " & _
        '"HT.exploracion_analgesia as exploracion_analgesia ," & _
        '"HT.exploracion_propiocepsion as exploracion_propiocepsion ," & _
        '"HT.exploracion_desinflamacion as exploracion_desinflamacion ," & _
        '"HT.exploracion_habilidades_manuales as exploracion_habilidades_manuales ," & _
        '"HT.exploracion_fortalecimiento as exploracion_fortalecimiento ," & _
        '"HT.exploracion_aumentar_rangos as exploracion_aumentar_rangos ," & _
        '"HT.exploracion_reduccion_marcha as exploracion_reduccion_marcha ," & _
        '"HT.exploracion_reintegracion_deportiva as exploracion_reintegracion_deportiva ," & _
        '"HT.analgesia_laser as analgesia_laser ," & _
        '"HT.analgesia_ultrasonido as analgesia_ultrasonido ," & _
        '"HT.analgesia_traccion_cervical as analgesia_traccion_cervical ," & _
        '"HT.analgesia_electroterapia as analgesia_electroterapia ," & _
        '"HT.analgesia_masaje as analgesia_masaje ," & _
        '"HT.analgesia_traccion_lumbar as analgesia_traccion_lumbar ," & _
        '"HT.analgesia_tape as analgesia_tape ," & _
        '"HT.analgesia_chc as analgesia_chc ," & _
        '"HT.analgesia_parafina as analgesia_parafina ," & _
        '"HT.analgesia_magneto as analgesia_magneto ," & _
        '"HT.analgesia_crio as analgesia_crio ," & _
        '"HT.analgesia_diatermia as analgesia_diatermia ," & _
        '"HT.analgesia_ondas as analgesia_ondas ," & _
        '"HT.analgesia_hidroterapia as analgesia_hidroterapia ," & _
        '"HT.analgesia_terapia_manual as analgesia_terapia_manual ," & _
        '"HT.analgesia_banios_contraste as analgesia_banios_contraste ," & _
        '"HT.analgesia_cf as analgesia_cf ," & _
        '"HT.analgesia_ejercicio as analgesia_ejercicio ," & _
        '"HT.analgesia_gimnasia as analgesia_gimnasia ," & _
        '"HT.estiramiento_sel as estiramiento_sel ," & _
        '"HT.estiramiento_activo_sup as estiramiento_activo_sup ," & _
        '"HT.estiramiento_pasivo_sup as estiramiento_pasivo_sup ," & _
        '"HT.estiramiento_cintura_escapular as estiramiento_cintura_escapular ," & _
        '"HT.estiramiento_extensores_muneca as estiramiento_extensores_muneca ," & _
        '"HT.estiramiento_extensores_codo as estiramiento_extensores_codo ," & _
        '"HT.estiramiento_flexores_muneca as estiramiento_flexores_muneca ," & _
        '"HT.estiramiento_flexores_codo as estiramiento_flexores_codo ," & _
        '"HT.estiramiento_maq_mov_pasiva_hombro as estiramiento_maq_mov_pasiva_hombro ," & _
        '"HT.estiramiento_mano_hombro as estiramiento_mano_hombro ," & _
        '"HT.estiramiento_superior_3_5 as estiramiento_superior_3_5 ," & _
        '"HT.estiramiento_superior_3_10 as estiramiento_superior_3_10 ," & _
        '"HT.estiramiento_superior_3_15 as estiramiento_superior_3_15 ," & _
        '"HT.estiramiento_dedos as estiramiento_dedos ," & _
        '"HT.estiramiento_otro_estiramiento as estiramiento_otro_estiramiento ," & _
        '"HT.estiramiento_activo_columna as estiramiento_activo_columna ," & _
        '"HT.estiramiento_pasivo_columna as estiramiento_pasivo_columna ," & _
        '"HT.estiramiento_paravertebrales_dorsales as estiramiento_paravertebrales_dorsales ," & _
        '"HT.estiramiento_paravertebrales_lumbares as estiramiento_paravertebrales_lumbares ," & _
        '"HT.estiramiento_cervicales as estiramiento_cervicales ," & _
        '"HT.estiramiento_paravertebrales as estiramiento_paravertebrales ," & _
        '"HT.estiramiento_columna_3_5 as estiramiento_columna_3_5 ," & _
        '"HT.estiramiento_columna_3_10 as estiramiento_columna_3_10 ," & _
        '"HT.estiramiento_columna_3_15 as estiramiento_columna_3_15 ," & _
        '"HT.estiramiento_trapecio as estiramiento_trapecio ," & _
        '"HT.estiramiento_activo_inf as estiramiento_activo_inf ," & _
        '"HT.estiramiento_pasivo_inf as estiramiento_pasivo_inf ," & _
        '"HT.estiramiento_cuadriceps as estiramiento_cuadriceps ," & _
        '"HT.estiramiento_tensor_fascia_lata as estiramiento_tensor_fascia_lata ," & _
        '"HT.estiramiento_isquiotibiales as estiramiento_isquiotibiales ," & _
        '"HT.estiramiento_tibial_anterior as estiramiento_tibial_anterior ," & _
        '"HT.estiramiento_aductores as estiramiento_aductores ," & _
        '"HT.estiramiento_tibial_posterior as estiramiento_tibial_posterior ," & _
        '"HT.estiramiento_abductores as estiramiento_abductores ," & _
        '"HT.estiramiento_peroneos as estiramiento_peroneos ," & _
        '"HT.estiramiento_rotadores_cadera as estiramiento_rotadores_cadera ," & _
        '"HT.estiramiento_maq_mov_pasiva_rodilla as estiramiento_maq_mov_pasiva_rodilla ," & _
        '"HT.estiramiento_fascia_plantar as estiramiento_fascia_plantar ," & _
        '"HT.estiramiento_maq_mov_pasiva_tobillo as estiramiento_maq_mov_pasiva_tobillo ," & _
        '"HT.estiramiento_inferior_3_5 as estiramiento_inferior_3_5 ," & _
        '"HT.estiramiento_inferior_3_10 as estiramiento_inferior_3_10 ," & _
        '"HT.estiramiento_inferior_3_15 as estiramiento_inferior_3_15 ," & _
        '"HT.fortalecimiento_sel as fortalecimiento_sel ," & _
        '"HT.fortalecimiento_cintura_escapular as fortalecimiento_cintura_escapular ," & _
        '"HT.fortalecimiento_ligas as fortalecimiento_ligas ," & _
        '"HT.fortalecimiento_polainas as fortalecimiento_polainas ," & _
        '"HT.fortalecimiento_isometricas as fortalecimiento_isometricas ," & _
        '"HT.fortalecimiento_sin_peso as fortalecimiento_sin_peso ," & _
        '"HT.fortalecimiento_extensores_muneca as fortalecimiento_extensores_muneca ," & _
        '"HT.fortalecimiento_extensores_codo as fortalecimiento_extensores_codo ," & _
        '"HT.fortalecimiento_flexores_muneca as fortalecimiento_flexores_muneca ," & _
        '"HT.fortalecimiento_flexores_codo as fortalecimiento_flexores_codo ," & _
        '"HT.fortalecimiento_maq_mov_pasiva_hombro as fortalecimiento_maq_mov_pasiva_hombro ," & _
        '"HT.fortalecimiento_mano_hombro as fortalecimiento_mano_hombro ," & _
        '"HT.fortalecimiento_superior_3_5 as fortalecimiento_superior_3_5 ," & _
        '"HT.fortalecimiento_superior_3_10 as fortalecimiento_superior_3_10 ," & _
        '"HT.fortalecimiento_superior_3_15 as fortalecimiento_superior_3_15 ," & _
        '"HT.fortalecimiento_dedos as fortalecimiento_dedos ," & _
        '"HT.fortalecimiento_otro_estiramiento as fortalecimiento_otro_estiramiento ," & _
        '"HT.fortalecimiento_paravertebrales_dorsales as fortalecimiento_paravertebrales_dorsales ," & _
        '"HT.fortalecimiento_williams as fortalecimiento_williams ," & _
        '"HT.fortalecimiento_mckenzic as fortalecimiento_mckenzic ," & _
        '"HT.fortalecimiento_core as fortalecimiento_core ," & _
        '"HT.fortalecimiento_klapp as fortalecimiento_klapp ," & _
        '"HT.fortalecimiento_paravertebrales_lumbares as fortalecimiento_paravertebrales_lumbares ," & _
        '"HT.fortalecimiento_cervicales as fortalecimiento_cervicales ," & _
        '"HT.fortalecimiento_paravertebrales as fortalecimiento_paravertebrales ," & _
        '"HT.fortalecimiento_columna_3_5 as fortalecimiento_columna_3_5 ," & _
        '"HT.fortalecimiento_columna_3_10 as fortalecimiento_columna_3_10 ," & _
        '"HT.fortalecimiento_columna_3_15 as fortalecimiento_columna_3_15 ," & _
        '"HT.fortalecimiento_trapecio as fortalecimiento_trapecio ," & _
        '"HT.fortalecimiento_ligas_columna as fortalecimiento_ligas_columna ," & _
        '"HT.fortalecimiento_isometricas_columna as fortalecimiento_isometricas_columna ," & _
        '"HT.fortalecimiento_cuadriceps as fortalecimiento_cuadriceps ," & _
        '"HT.fortalecimiento_ligas_inf as fortalecimiento_ligas_inf ," & _
        '"HT.fortalecimiento_isometricas_inf as fortalecimiento_isometricas_inf ," & _
        '"HT.fortalecimiento_tensor_fascia_lata as fortalecimiento_tensor_fascia_lata ," & _
        '"HT.fortalecimiento_isquiotibiales as fortalecimiento_isquiotibiales ," & _
        '"HT.fortalecimiento_tibial_anterior as fortalecimiento_tibial_anterior ," & _
        '"HT.fortalecimiento_aductores as fortalecimiento_aductores ," & _
        '"HT.fortalecimiento_tibial_posterior as fortalecimiento_tibial_posterior ," & _
        '"HT.fortalecimiento_abductores as fortalecimiento_abductores ," & _
        '"HT.fortalecimiento_peroneos as fortalecimiento_peroneos ," & _
        '"HT.fortalecimiento_rotadores_cadera as fortalecimiento_rotadores_cadera ," & _
        '"HT.fortalecimiento_fascia_plantar as fortalecimiento_fascia_plantar ," & _
        '"HT.fortalecimiento_polainas_inf as fortalecimiento_polainas_inf ," & _
        '"HT.fortalecimiento_sin_peso_inf as fortalecimiento_sin_peso_inf ," & _
        '"HT.fortalecimiento_banco as fortalecimiento_banco ," & _
        '"HT.fortalecimiento_trampolin as fortalecimiento_trampolin ," & _
        '"HT.fortalecimiento_bossu as fortalecimiento_bossu ," & _
        '"HT.fortalecimiento_inferior_3_5 as fortalecimiento_inferior_3_5 ," & _
        '"HT.fortalecimiento_inferior_3_10 as fortalecimiento_inferior_3_10 ," & _
        '"HT.fortalecimiento_inferior_3_15 as fortalecimiento_inferior_3_15 ," & _
        '"HT.reacondicionamiento_bici as reacondicionamiento_bici ," & _
        '"HT.reacondicionamiento_tiempo_bici as reacondicionamiento_tiempo_bici ," & _
        '"HT.reacondicionamiento_caminadora as reacondicionamiento_caminadora ," & _
        '"HT.reacondicionamiento_tiempo_caminadora as reacondicionamiento_tiempo_caminadora ," & _
        '"HT.reacondicionamiento_reduccion_marcha as reacondicionamiento_reduccion_marcha ," & _
        '"HT.reacondicionamiento_eliptica as reacondicionamiento_eliptica ," & _
        '"HT.reacondicionamiento_escaleras as reacondicionamiento_escaleras ," & _
        '"HT.reacondicionamiento_pelotas as reacondicionamiento_pelotas ," & _
        '"isnull(HT.observaciones,'') as observaciones ," & _
        '"isnull(TP.Nombre,'') as atendio " & _
        '  "FROM HMTratamientos HT " & _
        '  "left join Terapistas TP on TP.id=HT.idatendio " & _
        '  "where idcita='" + idcitafecha + "' "
        
        strSql = " select P.idcliente as Id,(P.paterno+' '+P.materno+' '+P.nombre) as paciente,P.edad as edad,p.domicilio as direccion, p.ciudadorigen as origen, CO.descripcion as ocupacion,p.fechanacimiento as fechanacimiento, " & _
       "T.IMedicas as IMedicas, T.CIndicaciones as CIndicaciones,AM.AlegiasPadecimientos as AlegiasPadecimientos, " & _
       "A.Fecha as FechaAgenda, " & _
     "isnull(HT.exploracion_fisica,'') as exploracion_fisica ," & _
     "isnull(HT.exploracion_analgesia,0) as exploracion_analgesia ," & _
     "isnull(HT.exploracion_propiocepsion,0) as exploracion_propiocepsion ," & _
     "isnull(HT.exploracion_desinflamacion,0) as exploracion_desinflamacion ," & _
     "isnull(HT.exploracion_habilidades_manuales,0) as exploracion_habilidades_manuales ," & _
     "isnull(HT.exploracion_fortalecimiento,0) as exploracion_fortalecimiento ," & _
     "isnull(HT.exploracion_aumentar_rangos,0) as exploracion_aumentar_rangos ," & _
     "isnull(HT.exploracion_reduccion_marcha,0) as exploracion_reduccion_marcha ," & _
     "isnull(HT.exploracion_reintegracion_deportiva,0) as exploracion_reintegracion_deportiva ," & _
     "isnull(HT.analgesia_laser,0) as analgesia_laser ," & _
     "isnull(HT.analgesia_ultrasonido,0) as analgesia_ultrasonido ," & _
     "isnull(HT.analgesia_traccion_cervical,0) as analgesia_traccion_cervical ," & _
     "isnull(HT.analgesia_electroterapia,0) as analgesia_electroterapia ," & _
     "isnull(HT.analgesia_masaje,0) as analgesia_masaje ," & _
     "isnull(HT.analgesia_traccion_lumbar,0) as analgesia_traccion_lumbar ," & _
     "isnull(HT.analgesia_tape,0) as analgesia_tape ," & _
     "isnull(HT.analgesia_chc,0) as analgesia_chc ," & _
     "isnull(HT.analgesia_parafina,0) as analgesia_parafina ," & _
     "isnull(HT.analgesia_magneto,0) as analgesia_magneto ," & _
     "isnull(HT.analgesia_crio,0) as analgesia_crio ," & _
     "isnull(HT.analgesia_diatermia,0) as analgesia_diatermia ," & _
     "isnull(HT.analgesia_ondas,0) as analgesia_ondas ," & _
     "isnull(HT.analgesia_hidroterapia,0) as analgesia_hidroterapia ," & _
     "isnull(HT.analgesia_terapia_manual,0) as analgesia_terapia_manual ," & _
     "isnull(HT.analgesia_banios_contraste,0) as analgesia_banios_contraste ," & _
     "isnull(HT.analgesia_cf,0) as analgesia_cf ," & _
     "isnull(HT.analgesia_ejercicio,0) as analgesia_ejercicio ," & _
     "isnull(HT.analgesia_gimnasia,0) as analgesia_gimnasia ," & _
     "isnull(HT.estiramiento_sel,0) as estiramiento_sel ," & _
     "isnull(HT.miembro_superior,0) as miembro_superior ," & _
      "isnull(HT.columna,0) as columna ," & _
      "isnull(HT.miembro_inferior,0) as miembro_inferior ," & _
     "isnull(HT.estiramiento_activo_sup,0) as estiramiento_activo_sup ," & _
     "isnull(HT.estiramiento_pasivo_sup,0) as estiramiento_pasivo_sup ," & _
     "isnull(HT.estiramiento_cintura_escapular,0) as estiramiento_cintura_escapular ," & _
     "isnull(HT.estiramiento_extensores_muneca,0) as estiramiento_extensores_muneca ," & _
     "isnull(HT.estiramiento_extensores_codo,0) as estiramiento_extensores_codo ," & _
     "isnull(HT.estiramiento_flexores_muneca,0) as estiramiento_flexores_muneca ," & _
     "isnull(HT.estiramiento_flexores_codo,0) as estiramiento_flexores_codo ," & _
     "isnull(HT.estiramiento_maq_mov_pasiva_hombro,0) as estiramiento_maq_mov_pasiva_hombro ," & _
     "isnull(HT.estiramiento_mano_hombro,0) as estiramiento_mano_hombro ," & _
     "isnull(HT.estiramiento_superior_3_5,0) as estiramiento_superior_3_5 ," & _
     "isnull(HT.estiramiento_superior_3_10,0) as estiramiento_superior_3_10 ," & _
     "isnull(HT.estiramiento_superior_3_15,0) as estiramiento_superior_3_15 ," & _
     "isnull(HT.estiramiento_dedos,0) as estiramiento_dedos ," & _
     "isnull(HT.estiramiento_otro_superior,'') as estiramiento_otro_superior ," & _
     "isnull(HT.estiramiento_otro_columna,'') as estiramiento_otro_columna ," & _
     "isnull(HT.estiramiento_otro_inferior,'') as estiramiento_otro_inferior ," & _
     "isnull(HT.estiramiento_activo_columna,0) as estiramiento_activo_columna ," & _
     "isnull(HT.estiramiento_pasivo_columna,0) as estiramiento_pasivo_columna ," & _
     "isnull(HT.estiramiento_paravertebrales_dorsales,0) as estiramiento_paravertebrales_dorsales ," & _
     "isnull(HT.estiramiento_paravertebrales_lumbares,0) as estiramiento_paravertebrales_lumbares ," & _
     "isnull(HT.estiramiento_cervicales,0) as estiramiento_cervicales ," & _
     "isnull(HT.estiramiento_paravertebrales,0) as estiramiento_paravertebrales ," & _
     "isnull(HT.estiramiento_columna_3_5,0) as estiramiento_columna_3_5 ," & _
     "isnull(HT.estiramiento_columna_3_10,0) as estiramiento_columna_3_10 ," & _
     "isnull(HT.estiramiento_columna_3_15,0) as estiramiento_columna_3_15 ," & _
     "isnull(HT.estiramiento_trapecio,0) as estiramiento_trapecio ," & _
     "isnull(HT.estiramiento_activo_inf,0) as estiramiento_activo_inf ," & _
     "isnull(HT.estiramiento_pasivo_inf,0) as estiramiento_pasivo_inf ," & _
     "isnull(HT.estiramiento_cuadriceps,0) as estiramiento_cuadriceps ," & _
     "isnull(HT.estiramiento_tensor_fascia_lata,0) as estiramiento_tensor_fascia_lata ," & _
     "isnull(HT.estiramiento_isquiotibiales,0) as estiramiento_isquiotibiales ," & _
     "isnull(HT.estiramiento_tibial_anterior,0) as estiramiento_tibial_anterior ," & _
     "isnull(HT.estiramiento_aductores,0) as estiramiento_aductores ," & _
     "isnull(HT.estiramiento_tibial_posterior,0) as estiramiento_tibial_posterior ," & _
     "isnull(HT.estiramiento_abductores,0) as estiramiento_abductores ," & _
     "isnull(HT.estiramiento_peroneos,0) as estiramiento_peroneos ," & _
     "isnull(HT.estiramiento_rotadores_cadera,0) as estiramiento_rotadores_cadera ," & _
     "isnull(HT.estiramiento_maq_mov_pasiva_rodilla,0) as estiramiento_maq_mov_pasiva_rodilla ," & _
     "isnull(HT.estiramiento_fascia_plantar,0) as estiramiento_fascia_plantar ," & _
     "isnull(HT.estiramiento_maq_mov_pasiva_tobillo,0) as estiramiento_maq_mov_pasiva_tobillo ," & _
     "isnull(HT.estiramiento_inferior_3_5,0) as estiramiento_inferior_3_5 ," & _
     "isnull(HT.estiramiento_inferior_3_10,0) as estiramiento_inferior_3_10 ," & _
     "isnull(HT.estiramiento_inferior_3_15,0) as estiramiento_inferior_3_15 ," & _
     "isnull(HT.fortalecimiento_sel,0) as fortalecimiento_sel ," & _
     "isnull(HT.fortalecimiento_cintura_escapular,0) as fortalecimiento_cintura_escapular ," & _
     "isnull(HT.fortalecimiento_ligas,0) as fortalecimiento_ligas ," & _
     "isnull(HT.fortalecimiento_polainas,0) as fortalecimiento_polainas ," & _
     "isnull(HT.fortalecimiento_isometricas,0) as fortalecimiento_isometricas ," & _
     "isnull(HT.fortalecimiento_sin_peso,0) as fortalecimiento_sin_peso ," & _
     "isnull(HT.fortalecimiento_extensores_muneca,0) as fortalecimiento_extensores_muneca ," & _
     "isnull(HT.fortalecimiento_extensores_codo,0) as fortalecimiento_extensores_codo ," & _
     "isnull(HT.fortalecimiento_flexores_muneca,0) as fortalecimiento_flexores_muneca ," & _
     "isnull(HT.fortalecimiento_flexores_codo,0) as fortalecimiento_flexores_codo ," & _
     "isnull(HT.fortalecimiento_maq_mov_pasiva_hombro,0) as fortalecimiento_maq_mov_pasiva_hombro ," & _
     "isnull(HT.fortalecimiento_mano_hombro,0) as fortalecimiento_mano_hombro ," & _
     "isnull(HT.fortalecimiento_superior_3_5,0) as fortalecimiento_superior_3_5 ," & _
     "isnull(HT.fortalecimiento_superior_3_10,0) as fortalecimiento_superior_3_10 ," & _
     "isnull(HT.fortalecimiento_superior_3_15,0) as fortalecimiento_superior_3_15 ," & _
     "isnull(HT.fortalecimiento_dedos,0) as fortalecimiento_dedos ," & _
     "isnull(HT.fortalecimiento_otro_superior,'') as fortalecimiento_otro_superior ," & _
      "isnull(HT.fortalecimiento_otro_columna,'') as fortalecimiento_otro_columna ," & _
      "isnull(HT.fortalecimiento_otro_inferior,'') as fortalecimiento_otro_inferior ," & _
     "isnull(HT.fortalecimiento_paravertebrales_dorsales,0) as fortalecimiento_paravertebrales_dorsales ," & _
     "isnull(HT.fortalecimiento_williams,0) as fortalecimiento_williams ," & _
     "isnull(HT.fortalecimiento_mckenzic,0) as fortalecimiento_mckenzic ," & _
     "isnull(HT.fortalecimiento_core,0) as fortalecimiento_core ," & _
     "isnull(HT.fortalecimiento_klapp,0) as fortalecimiento_klapp ," & _
     "isnull(HT.fortalecimiento_paravertebrales_lumbares,0) as fortalecimiento_paravertebrales_lumbares ," & _
     "isnull(HT.fortalecimiento_cervicales,0) as fortalecimiento_cervicales ," & _
     "isnull(HT.fortalecimiento_paravertebrales,0) as fortalecimiento_paravertebrales ," & _
     "isnull(HT.fortalecimiento_columna_3_5,0) as fortalecimiento_columna_3_5 ," & _
     "isnull(HT.fortalecimiento_columna_3_10,0) as fortalecimiento_columna_3_10 ," & _
     "isnull(HT.fortalecimiento_columna_3_15,0) as fortalecimiento_columna_3_15 ," & _
     "isnull(HT.fortalecimiento_trapecio,0) as fortalecimiento_trapecio ," & _
     "isnull(HT.fortalecimiento_ligas_columna,0) as fortalecimiento_ligas_columna ," & _
     "isnull(HT.fortalecimiento_isometricas_columna,0) as fortalecimiento_isometricas_columna ," & _
     "isnull(HT.fortalecimiento_cuadriceps,0) as fortalecimiento_cuadriceps ," & _
     "isnull(HT.fortalecimiento_ligas_inf,0) as fortalecimiento_ligas_inf ," & _
     "isnull(HT.fortalecimiento_isometricas_inf,0) as fortalecimiento_isometricas_inf ," & _
     "isnull(HT.fortalecimiento_tensor_fascia_lata,0) as fortalecimiento_tensor_fascia_lata ," & _
     "isnull(HT.fortalecimiento_isquiotibiales,0) as fortalecimiento_isquiotibiales ," & _
     "isnull(HT.fortalecimiento_tibial_anterior,0) as fortalecimiento_tibial_anterior ," & _
     "isnull(HT.fortalecimiento_aductores,0) as fortalecimiento_aductores ," & _
     "isnull(HT.fortalecimiento_tibial_posterior,0) as fortalecimiento_tibial_posterior ," & _
     "isnull(HT.fortalecimiento_abductores,0) as fortalecimiento_abductores ," & _
     "isnull(HT.fortalecimiento_peroneos,0) as fortalecimiento_peroneos ," & _
     "isnull(HT.fortalecimiento_rotadores_cadera,0) as fortalecimiento_rotadores_cadera ," & _
     "isnull(HT.fortalecimiento_fascia_plantar,0) as fortalecimiento_fascia_plantar ," & _
     "isnull(HT.fortalecimiento_polainas_inf,0) as fortalecimiento_polainas_inf ," & _
     "isnull(HT.fortalecimiento_sin_peso_inf,0) as fortalecimiento_sin_peso_inf ," & _
     "isnull(HT.fortalecimiento_banco,0) as fortalecimiento_banco ," & _
     "isnull(HT.fortalecimiento_trampolin,0) as fortalecimiento_trampolin ," & _
     "isnull(HT.fortalecimiento_bossu,0) as fortalecimiento_bossu ," & _
     "isnull(HT.fortalecimiento_inferior_3_5,0) as fortalecimiento_inferior_3_5 ," & _
     "isnull(HT.fortalecimiento_inferior_3_10,0) as fortalecimiento_inferior_3_10 ," & _
     "isnull(HT.fortalecimiento_inferior_3_15,0) as fortalecimiento_inferior_3_15 ," & _
     "isnull(HT.reacondicionamiento_bici,0) as reacondicionamiento_bici ," & _
     "isnull(HT.reacondicionamiento_tiempo_bici,0) as reacondicionamiento_tiempo_bici ," & _
     "isnull(HT.reacondicionamiento_caminadora,0) as reacondicionamiento_caminadora ," & _
     "isnull(HT.reacondicionamiento_tiempo_caminadora,0) as reacondicionamiento_tiempo_caminadora ," & _
     "isnull(HT.reacondicionamiento_reduccion_marcha,0) as reacondicionamiento_reduccion_marcha ," & _
     "isnull(HT.reacondicionamiento_eliptica,0) as reacondicionamiento_eliptica ," & _
     "isnull(HT.reacondicionamiento_escaleras,0) as reacondicionamiento_escaleras ," & _
     "isnull(HT.reacondicionamiento_pelotas,0) as reacondicionamiento_pelotas ," & _
     "isnull(HT.observaciones,'') as observaciones ," & _
     "isnull(HT.analgesia_laser_otro,'') as analgesia_laser_otro ," & _
     "isnull(HT.analgesia_ultrasonido_otro,'') as analgesia_ultrasonido_otro ," & _
     "isnull(HT.analgesia_electroterapia_otro,'') as analgesia_electroterapia_otro ," & _
     "isnull(TP.Nombre,'') as atendio " & _
       "from agenda A " & _
       "inner join clientes P on P.idcliente=A.idcliente " & _
       "left join terapia T on T.idcliente=A.idcliente " & _
       "left join HMTratamientos HT on HT.idcita=A.idcita " & _
       "left join Terapistas TP on TP.id=HT.idatendio " & _
       "left join CatOcupaciones CO on CO.IdOcupacion=P.idocupacion " & _
       "left join HMAntecedentesMedicos AM on AM.idCliente=A.idCliente " & _
       "where A.idCita='" + idcitafecha + "' "
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                
                strSql2 = "SELECT A.idcita as idcita,format(A.fecha,'dd/MM/yyyy') as fecha  FROM agenda A " & _
                " where A.idcliente = '" & dt.Rows(0).Item("Id") & "' AND estado<>'CANCELADO' and convert(varchar(10),((select fecha from agenda where Idcita = '" + idcita + "')),103)>=A.fecha " & _
                " order by A.fecha desc "
                If clsDatos.cargatabla(strSql2, dt2) = 0 Then
                    If dt.Rows.Count > 0 Then
                        Dim FechaA As String
                        Dim IdcitaA As String
                        Dim count2 As Integer = 1
                        For Each F As DataRow In dt2.Rows
                            If count2 > 1 Then
                                FechaA += "."
                                IdcitaA += "."
                            End If
                            count2 = count2 + 1
                            If Not IsDBNull(F.Item("fecha")) Then
                                FechaA += F.Item("fecha")
                            End If
                            If Not IsDBNull(F.Item("idcita")) Then
                                IdcitaA += F.Item("idcita").ToString
                            End If
                            
                            
                            
                        Next
                        Fechas = FechaA
                        icita = IdcitaA
                    End If
                Else
                    'FechaA
                End If
                
                strSql3 = "SELECT id, Nombre  FROM Terapistas T where turno='" + turno + "' " & _
              " order by id Asc "
                If clsDatos.cargatabla(strSql3, dt3) = 0 Then
                    If dt.Rows.Count > 0 Then
                        Dim Terapista As String
                        Dim IdTerapista As String
                        Dim countT As Integer = 1
                        For Each T As DataRow In dt3.Rows
                            If countT > 1 Then
                                Terapista += "."
                                IdTerapista += "."
                            End If
                            countT = countT + 1
                            If Not IsDBNull(T.Item("Nombre")) Then
                                Terapista += T.Item("Nombre")
                            End If
                            If Not IsDBNull(T.Item("id")) Then
                                IdTerapista += T.Item("id").ToString
                            End If
                            
                            
                            
                        Next
                        CTerapista = Terapista
                        CidTerapista = IdTerapista
                    End If
                Else
                    'TERAPISTA
                End If
                
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    'cadena += "{""id"":""" & i.Item("idtratamiento") & """, ""ultrasonido"":""" & i.Item("ultrasonido") & """, ""chc"":""" & i.Item("chc") & """, ""electroterapia"":""" & i.Item("electroterapia") & """, ""laser"":""" & i.Item("laser") & """, ""cf"":""" & i.Item("cf") & """, ""ejercicio"":""" & i.Item("ejercicio") & """,""masaje"":""" & i.Item("masaje") & """,""magneto"":""" & i.Item("magneto") & """,""gimnasia"":""" & i.Item("gimnasia") & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """}"
                    'cadena += "{""id"":""" & i.Item("idtratamiento") & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """,""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""exploracion_analgesia"" : """ & i.Item("exploracion_analgesia") & """,""exploracion_propiocepsion"" : """ & i.Item("exploracion_propiocepsion") & """,""exploracion_desinflamacion"" : """ & i.Item("exploracion_desinflamacion") & """,""exploracion_habilidades_manuales"" : """ & i.Item("exploracion_habilidades_manuales") & """,""exploracion_fortalecimiento"" : """ & i.Item("exploracion_fortalecimiento") & """,""exploracion_aumentar_rangos"" : """ & i.Item("exploracion_aumentar_rangos") & """,""exploracion_reduccion_marcha"" : """ & i.Item("exploracion_reduccion_marcha") & """,""exploracion_reintegracion_deportiva"" : """ & i.Item("exploracion_reintegracion_deportiva") & """,""analgesia_laser"" : """ & i.Item("analgesia_laser") & """,""analgesia_ultrasonido"" : """ & i.Item("analgesia_ultrasonido") & """,""analgesia_traccion_cervical"" : """ & i.Item("analgesia_traccion_cervical") & """,""analgesia_electroterapia"" : """ & i.Item("analgesia_electroterapia") & """,""analgesia_masaje"" : """ & i.Item("analgesia_masaje") & """,""analgesia_traccion_lumbar"" : """ & i.Item("analgesia_traccion_lumbar") & """,""analgesia_tape"" : """ & i.Item("analgesia_tape") & """,""analgesia_chc"" : """ & i.Item("analgesia_chc") & """,""analgesia_parafina"" : """ & i.Item("analgesia_parafina") & """,""analgesia_magneto"" : """ & i.Item("analgesia_magneto") & """,""analgesia_crio"" : """ & i.Item("analgesia_crio") & """,""analgesia_diatermia"" : """ & i.Item("analgesia_diatermia") & """,""analgesia_ondas"" : """ & i.Item("analgesia_ondas") & """,""analgesia_hidroterapia"" : """ & i.Item("analgesia_hidroterapia") & """,""analgesia_terapia_manual"" : """ & i.Item("analgesia_terapia_manual") & """,""analgesia_banios_contraste"" : """ & i.Item("analgesia_banios_contraste") & """,""analgesia_cf"" : """ & i.Item("analgesia_cf") & """,""analgesia_ejercicio"" : """ & i.Item("analgesia_ejercicio") & """,""analgesia_gimnasia"" : """ & i.Item("analgesia_gimnasia") & """,""estiramiento_sel"" : """ & i.Item("estiramiento_sel") & """,""estiramiento_activo_sup"" : """ & i.Item("estiramiento_activo_sup") & """,""estiramiento_pasivo_sup"" : """ & i.Item("estiramiento_pasivo_sup") & """,""estiramiento_cintura_escapular"" : """ & i.Item("estiramiento_cintura_escapular") & """,""estiramiento_extensores_muneca"" : """ & i.Item("estiramiento_extensores_muneca") & """,""estiramiento_extensores_codo"" : """ & i.Item("estiramiento_extensores_codo") & """,""estiramiento_flexores_muneca"" : """ & i.Item("estiramiento_flexores_muneca") & """,""estiramiento_flexores_codo"" : """ & i.Item("estiramiento_flexores_codo") & """,""estiramiento_maq_mov_pasiva_hombro"" : """ & i.Item("estiramiento_maq_mov_pasiva_hombro") & """,""estiramiento_mano_hombro"" : """ & i.Item("estiramiento_mano_hombro") & """,""estiramiento_superior_3_5"" : """ & i.Item("estiramiento_superior_3_5") & """,""estiramiento_superior_3_10"" : """ & i.Item("estiramiento_superior_3_10") & """,""estiramiento_superior_3_15"" : """ & i.Item("estiramiento_superior_3_15") & """,""estiramiento_dedos"" : """ & i.Item("estiramiento_dedos") & """,""estiramiento_otro_estiramiento"" : """ & i.Item("estiramiento_otro_estiramiento") & """,""estiramiento_activo_columna"" : """ & i.Item("estiramiento_activo_columna") & """,""estiramiento_pasivo_columna"" : """ & i.Item("estiramiento_pasivo_columna") & """,""estiramiento_paravertebrales_dorsales"" : """ & i.Item("estiramiento_paravertebrales_dorsales") & """,""estiramiento_paravertebrales_lumbares"" : """ & i.Item("estiramiento_paravertebrales_lumbares") & """,""estiramiento_cervicales"" : """ & i.Item("estiramiento_cervicales") & """,""estiramiento_paravertebrales"" : """ & i.Item("estiramiento_paravertebrales") & """,""estiramiento_columna_3_5"" : """ & i.Item("estiramiento_columna_3_5") & """,""estiramiento_columna_3_10"" : """ & i.Item("estiramiento_columna_3_10") & """,""estiramiento_columna_3_15"" : """ & i.Item("estiramiento_columna_3_15") & """,""estiramiento_trapecio"" : """ & i.Item("estiramiento_trapecio") & """,""estiramiento_activo_inf"" : """ & i.Item("estiramiento_activo_inf") & """,""estiramiento_pasivo_inf"" : """ & i.Item("estiramiento_pasivo_inf") & """,""estiramiento_cuadriceps"" : """ & i.Item("estiramiento_cuadriceps") & """,""estiramiento_tensor_fascia_lata"" : """ & i.Item("estiramiento_tensor_fascia_lata") & """,""estiramiento_isquiotibiales"" : """ & i.Item("estiramiento_isquiotibiales") & """,""estiramiento_tibial_anterior"" : """ & i.Item("estiramiento_tibial_anterior") & """,""estiramiento_aductores"" : """ & i.Item("estiramiento_aductores") & """,""estiramiento_tibial_posterior"" : """ & i.Item("estiramiento_tibial_posterior") & """,""estiramiento_abductores"" : """ & i.Item("estiramiento_abductores") & """,""estiramiento_peroneos"" : """ & i.Item("estiramiento_peroneos") & """,""estiramiento_rotadores_cadera"" : """ & i.Item("estiramiento_rotadores_cadera") & """,""estiramiento_maq_mov_pasiva_rodilla"" : """ & i.Item("estiramiento_maq_mov_pasiva_rodilla") & """,""estiramiento_fascia_plantar"" : """ & i.Item("estiramiento_fascia_plantar") & """,""estiramiento_maq_mov_pasiva_tobillo"" : """ & i.Item("estiramiento_maq_mov_pasiva_tobillo") & """,""estiramiento_inferior_3_5"" : """ & i.Item("estiramiento_inferior_3_5") & """,""estiramiento_inferior_3_10"" : """ & i.Item("estiramiento_inferior_3_10") & """,""estiramiento_inferior_3_15"" : """ & i.Item("estiramiento_inferior_3_15") & """,""fortalecimiento_sel"" : """ & i.Item("fortalecimiento_sel") & """,""fortalecimiento_cintura_escapular"" : """ & i.Item("fortalecimiento_cintura_escapular") & """,""fortalecimiento_ligas"" : """ & i.Item("fortalecimiento_ligas") & """,""fortalecimiento_polainas"" : """ & i.Item("fortalecimiento_polainas") & """,""fortalecimiento_isometricas"" : """ & i.Item("fortalecimiento_isometricas") & """,""fortalecimiento_sin_peso"" : """ & i.Item("fortalecimiento_sin_peso") & """,""fortalecimiento_extensores_muneca"" : """ & i.Item("fortalecimiento_extensores_muneca") & """,""fortalecimiento_extensores_codo"" : """ & i.Item("fortalecimiento_extensores_codo") & """,""fortalecimiento_flexores_muneca"" : """ & i.Item("fortalecimiento_flexores_muneca") & """,""fortalecimiento_flexores_codo"" : """ & i.Item("fortalecimiento_flexores_codo") & """,""fortalecimiento_maq_mov_pasiva_hombro"" : """ & i.Item("fortalecimiento_maq_mov_pasiva_hombro") & """,""fortalecimiento_mano_hombro"" : """ & i.Item("fortalecimiento_mano_hombro") & """,""fortalecimiento_superior_3_5"" : """ & i.Item("fortalecimiento_superior_3_5") & """,""fortalecimiento_superior_3_10"" : """ & i.Item("fortalecimiento_superior_3_10") & """,""fortalecimiento_superior_3_15"" : """ & i.Item("fortalecimiento_superior_3_15") & """,""fortalecimiento_dedos"" : """ & i.Item("fortalecimiento_dedos") & """,""fortalecimiento_otro_estiramiento"" : """ & i.Item("fortalecimiento_otro_estiramiento") & """,""fortalecimiento_paravertebrales_dorsales"" : """ & i.Item("fortalecimiento_paravertebrales_dorsales") & """,""fortalecimiento_williams"" : """ & i.Item("fortalecimiento_williams") & """,""fortalecimiento_mckenzic"" : """ & i.Item("fortalecimiento_mckenzic") & """,""fortalecimiento_core"" : """ & i.Item("fortalecimiento_core") & """,""fortalecimiento_klapp"" : """ & i.Item("fortalecimiento_klapp") & """,""fortalecimiento_paravertebrales_lumbares"" : """ & i.Item("fortalecimiento_paravertebrales_lumbares") & """,""fortalecimiento_cervicales"" : """ & i.Item("fortalecimiento_cervicales") & """,""fortalecimiento_paravertebrales"" : """ & i.Item("fortalecimiento_paravertebrales") & """,""fortalecimiento_columna_3_5"" : """ & i.Item("fortalecimiento_columna_3_5") & """,""fortalecimiento_columna_3_10"" : """ & i.Item("fortalecimiento_columna_3_10") & """,""fortalecimiento_columna_3_15"" : """ & i.Item("fortalecimiento_columna_3_15") & """,""fortalecimiento_trapecio"" : """ & i.Item("fortalecimiento_trapecio") & """,""fortalecimiento_ligas_columna"" : """ & i.Item("fortalecimiento_ligas_columna") & """,""fortalecimiento_isometricas_columna"" : """ & i.Item("fortalecimiento_isometricas_columna") & """,""fortalecimiento_cuadriceps"" : """ & i.Item("fortalecimiento_cuadriceps") & """,""fortalecimiento_ligas_inf"" : """ & i.Item("fortalecimiento_ligas_inf") & """,""fortalecimiento_isometricas_inf"" : """ & i.Item("fortalecimiento_isometricas_inf") & """,""fortalecimiento_tensor_fascia_lata"" : """ & i.Item("fortalecimiento_tensor_fascia_lata") & """,""fortalecimiento_isquiotibiales"" : """ & i.Item("fortalecimiento_isquiotibiales") & """,""fortalecimiento_tibial_anterior"" : """ & i.Item("fortalecimiento_tibial_anterior") & """,""fortalecimiento_aductores"" : """ & i.Item("fortalecimiento_aductores") & """,""fortalecimiento_tibial_posterior"" : """ & i.Item("fortalecimiento_tibial_posterior") & """,""fortalecimiento_abductores"" : """ & i.Item("fortalecimiento_abductores") & """,""fortalecimiento_peroneos"" : """ & i.Item("fortalecimiento_peroneos") & """,""fortalecimiento_rotadores_cadera"" : """ & i.Item("fortalecimiento_rotadores_cadera") & """,""fortalecimiento_fascia_plantar"" : """ & i.Item("fortalecimiento_fascia_plantar") & """,""fortalecimiento_polainas_inf"" : """ & i.Item("fortalecimiento_polainas_inf") & """,""fortalecimiento_sin_peso_inf"" : """ & i.Item("fortalecimiento_sin_peso_inf") & """,""fortalecimiento_banco"" : """ & i.Item("fortalecimiento_banco") & """,""fortalecimiento_trampolin"" : """ & i.Item("fortalecimiento_trampolin") & """,""fortalecimiento_bossu"" : """ & i.Item("fortalecimiento_bossu") & """,""fortalecimiento_inferior_3_5"" : """ & i.Item("fortalecimiento_inferior_3_5") & """,""fortalecimiento_inferior_3_10"" : """ & i.Item("fortalecimiento_inferior_3_10") & """,""fortalecimiento_inferior_3_15"" : """ & i.Item("fortalecimiento_inferior_3_15") & """,""reacondicionamiento_bici"" : """ & i.Item("reacondicionamiento_bici") & """,""reacondicionamiento_tiempo_bici"" : """ & i.Item("reacondicionamiento_tiempo_bici") & """,""reacondicionamiento_caminadora"" : """ & i.Item("reacondicionamiento_caminadora") & """,""reacondicionamiento_tiempo_caminadora"" : """ & i.Item("reacondicionamiento_tiempo_caminadora") & """,""reacondicionamiento_reduccion_marcha"" : """ & i.Item("reacondicionamiento_reduccion_marcha") & """,""reacondicionamiento_eliptica"" : """ & i.Item("reacondicionamiento_eliptica") & """,""reacondicionamiento_escaleras"" : """ & i.Item("reacondicionamiento_escaleras") & """,""reacondicionamiento_pelotas"" : """ & i.Item("reacondicionamiento_pelotas") & """}"
                    cadena += "{""id"":""" & i.Item("Id") & """,""idcitafecha"":""" & idcitafecha & """,""nombre"":""" & i.Item("paciente") & """,""fecha_nac"":""" & i.Item("fechanacimiento") & """,""edad"":""" & i.Item("edad") & """,""origen"":""" & i.Item("origen") & """,""ocupacion"":""" & i.Item("ocupacion") & """,""direccion"":""" & i.Item("direccion") & """,""alergias"":""" & i.Item("AlegiasPadecimientos") & """, ""indicaciones_medicas"":""" & i.Item("IMedicas") & """, ""contra_indicaciones"":""" & i.Item("CIndicaciones") & """ , ""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """,""exploracion_fisica"" : """ & i.Item("exploracion_fisica") & """,""exploracion_analgesia"" : """ & i.Item("exploracion_analgesia") & """,""exploracion_propiocepsion"" : """ & i.Item("exploracion_propiocepsion") & """,""exploracion_desinflamacion"" : """ & i.Item("exploracion_desinflamacion") & """,""exploracion_habilidades_manuales"" : """ & i.Item("exploracion_habilidades_manuales") & """,""exploracion_fortalecimiento"" : """ & i.Item("exploracion_fortalecimiento") & """,""exploracion_aumentar_rangos"" : """ & i.Item("exploracion_aumentar_rangos") & """,""exploracion_reduccion_marcha"" : """ & i.Item("exploracion_reduccion_marcha") & """,""exploracion_reintegracion_deportiva"" : """ & i.Item("exploracion_reintegracion_deportiva") & """,""analgesia_laser"" : """ & i.Item("analgesia_laser") & """,""analgesia_ultrasonido"" : """ & i.Item("analgesia_ultrasonido") & """,""analgesia_traccion_cervical"" : """ & i.Item("analgesia_traccion_cervical") & """,""analgesia_electroterapia"" : """ & i.Item("analgesia_electroterapia") & """,""analgesia_masaje"" : """ & i.Item("analgesia_masaje") & """,""analgesia_traccion_lumbar"" : """ & i.Item("analgesia_traccion_lumbar") & """,""analgesia_tape"" : """ & i.Item("analgesia_tape") & """,""analgesia_chc"" : """ & i.Item("analgesia_chc") & """,""analgesia_parafina"" : """ & i.Item("analgesia_parafina") & """,""analgesia_magneto"" : """ & i.Item("analgesia_magneto") & """,""analgesia_crio"" : """ & i.Item("analgesia_crio") & """,""analgesia_diatermia"" : """ & i.Item("analgesia_diatermia") & """,""analgesia_ondas"" : """ & i.Item("analgesia_ondas") & """,""analgesia_hidroterapia"" : """ & i.Item("analgesia_hidroterapia") & """,""analgesia_terapia_manual"" : """ & i.Item("analgesia_terapia_manual") & """,""analgesia_banios_contraste"" : """ & i.Item("analgesia_banios_contraste") & """,""analgesia_cf"" : """ & i.Item("analgesia_cf") & """,""analgesia_ejercicio"" : """ & i.Item("analgesia_ejercicio") & """,""analgesia_gimnasia"" : """ & i.Item("analgesia_gimnasia") & """,""estiramiento_sel"" : """ & i.Item("estiramiento_sel") & """,""miembro_superior"" : """ & i.Item("miembro_superior") & """,""columna"" : """ & i.Item("columna") & """,""miembro_inferior"" : """ & i.Item("miembro_inferior") & """,""estiramiento_activo_sup"" : """ & i.Item("estiramiento_activo_sup") & """,""estiramiento_pasivo_sup"" : """ & i.Item("estiramiento_pasivo_sup") & """,""estiramiento_cintura_escapular"" : """ & i.Item("estiramiento_cintura_escapular") & """,""estiramiento_extensores_muneca"" : """ & i.Item("estiramiento_extensores_muneca") & """,""estiramiento_extensores_codo"" : """ & i.Item("estiramiento_extensores_codo") & """,""estiramiento_flexores_muneca"" : """ & i.Item("estiramiento_flexores_muneca") & """,""estiramiento_flexores_codo"" : """ & i.Item("estiramiento_flexores_codo") & """,""estiramiento_maq_mov_pasiva_hombro"" : """ & i.Item("estiramiento_maq_mov_pasiva_hombro") & """,""estiramiento_mano_hombro"" : """ & i.Item("estiramiento_mano_hombro") & """,""estiramiento_superior_3_5"" : """ & i.Item("estiramiento_superior_3_5") & """,""estiramiento_superior_3_10"" : """ & i.Item("estiramiento_superior_3_10") & """,""estiramiento_superior_3_15"" : """ & i.Item("estiramiento_superior_3_15") & """,""estiramiento_dedos"" : """ & i.Item("estiramiento_dedos") & """,""estiramiento_otro_superior"" : """ & i.Item("estiramiento_otro_superior") & """,""estiramiento_otro_columna"" : """ & i.Item("estiramiento_otro_columna") & """,""estiramiento_otro_inferior"" : """ & i.Item("estiramiento_otro_inferior") & """,""estiramiento_activo_columna"" : """ & i.Item("estiramiento_activo_columna") & """,""estiramiento_pasivo_columna"" : """ & i.Item("estiramiento_pasivo_columna") & """,""estiramiento_paravertebrales_dorsales"" : """ & i.Item("estiramiento_paravertebrales_dorsales") & """,""estiramiento_paravertebrales_lumbares"" : """ & i.Item("estiramiento_paravertebrales_lumbares") & """,""estiramiento_cervicales"" : """ & i.Item("estiramiento_cervicales") & """,""estiramiento_paravertebrales"" : """ & i.Item("estiramiento_paravertebrales") & """,""estiramiento_columna_3_5"" : """ & i.Item("estiramiento_columna_3_5") & """,""estiramiento_columna_3_10"" : """ & i.Item("estiramiento_columna_3_10") & """,""estiramiento_columna_3_15"" : """ & i.Item("estiramiento_columna_3_15") & """,""estiramiento_trapecio"" : """ & i.Item("estiramiento_trapecio") & """,""estiramiento_activo_inf"" : """ & i.Item("estiramiento_activo_inf") & """,""estiramiento_pasivo_inf"" : """ & i.Item("estiramiento_pasivo_inf") & """,""estiramiento_cuadriceps"" : """ & i.Item("estiramiento_cuadriceps") & """,""estiramiento_tensor_fascia_lata"" : """ & i.Item("estiramiento_tensor_fascia_lata") & """,""estiramiento_isquiotibiales"" : """ & i.Item("estiramiento_isquiotibiales") & """,""estiramiento_tibial_anterior"" : """ & i.Item("estiramiento_tibial_anterior") & """,""estiramiento_aductores"" : """ & i.Item("estiramiento_aductores") & """,""estiramiento_tibial_posterior"" : """ & i.Item("estiramiento_tibial_posterior") & """,""estiramiento_abductores"" : """ & i.Item("estiramiento_abductores") & """,""estiramiento_peroneos"" : """ & i.Item("estiramiento_peroneos") & """,""estiramiento_rotadores_cadera"" : """ & i.Item("estiramiento_rotadores_cadera") & """,""estiramiento_maq_mov_pasiva_rodilla"" : """ & i.Item("estiramiento_maq_mov_pasiva_rodilla") & """,""estiramiento_fascia_plantar"" : """ & i.Item("estiramiento_fascia_plantar") & """,""estiramiento_maq_mov_pasiva_tobillo"" : """ & i.Item("estiramiento_maq_mov_pasiva_tobillo") & """,""estiramiento_inferior_3_5"" : """ & i.Item("estiramiento_inferior_3_5") & """,""estiramiento_inferior_3_10"" : """ & i.Item("estiramiento_inferior_3_10") & """,""estiramiento_inferior_3_15"" : """ & i.Item("estiramiento_inferior_3_15") & """,""fortalecimiento_sel"" : """ & i.Item("fortalecimiento_sel") & """,""fortalecimiento_cintura_escapular"" : """ & i.Item("fortalecimiento_cintura_escapular") & """,""fortalecimiento_ligas"" : """ & i.Item("fortalecimiento_ligas") & """,""fortalecimiento_polainas"" : """ & i.Item("fortalecimiento_polainas") & """,""fortalecimiento_isometricas"" : """ & i.Item("fortalecimiento_isometricas") & """,""fortalecimiento_sin_peso"" : """ & i.Item("fortalecimiento_sin_peso") & """,""fortalecimiento_extensores_muneca"" : """ & i.Item("fortalecimiento_extensores_muneca") & """,""fortalecimiento_extensores_codo"" : """ & i.Item("fortalecimiento_extensores_codo") & """,""fortalecimiento_flexores_muneca"" : """ & i.Item("fortalecimiento_flexores_muneca") & """,""fortalecimiento_flexores_codo"" : """ & i.Item("fortalecimiento_flexores_codo") & """,""fortalecimiento_maq_mov_pasiva_hombro"" : """ & i.Item("fortalecimiento_maq_mov_pasiva_hombro") & """,""fortalecimiento_mano_hombro"" : """ & i.Item("fortalecimiento_mano_hombro") & """,""fortalecimiento_superior_3_5"" : """ & i.Item("fortalecimiento_superior_3_5") & """,""fortalecimiento_superior_3_10"" : """ & i.Item("fortalecimiento_superior_3_10") & """,""fortalecimiento_superior_3_15"" : """ & i.Item("fortalecimiento_superior_3_15") & """,""fortalecimiento_dedos"" : """ & i.Item("fortalecimiento_dedos") & """,""fortalecimiento_otro_superior"" : """ & i.Item("fortalecimiento_otro_superior") & """,""fortalecimiento_otro_columna"" : """ & i.Item("fortalecimiento_otro_columna") & """,""fortalecimiento_otro_inferior"" : """ & i.Item("fortalecimiento_otro_inferior") & """,""fortalecimiento_paravertebrales_dorsales"" : """ & i.Item("fortalecimiento_paravertebrales_dorsales") & """,""fortalecimiento_williams"" : """ & i.Item("fortalecimiento_williams") & """,""fortalecimiento_mckenzic"" : """ & i.Item("fortalecimiento_mckenzic") & """,""fortalecimiento_core"" : """ & i.Item("fortalecimiento_core") & """,""fortalecimiento_klapp"" : """ & i.Item("fortalecimiento_klapp") & """,""fortalecimiento_paravertebrales_lumbares"" : """ & i.Item("fortalecimiento_paravertebrales_lumbares") & """,""fortalecimiento_cervicales"" : """ & i.Item("fortalecimiento_cervicales") & """,""fortalecimiento_paravertebrales"" : """ & i.Item("fortalecimiento_paravertebrales") & """,""fortalecimiento_columna_3_5"" : """ & i.Item("fortalecimiento_columna_3_5") & """,""fortalecimiento_columna_3_10"" : """ & i.Item("fortalecimiento_columna_3_10") & """,""fortalecimiento_columna_3_15"" : """ & i.Item("fortalecimiento_columna_3_15") & """,""fortalecimiento_trapecio"" : """ & i.Item("fortalecimiento_trapecio") & """,""fortalecimiento_ligas_columna"" : """ & i.Item("fortalecimiento_ligas_columna") & """,""fortalecimiento_isometricas_columna"" : """ & i.Item("fortalecimiento_isometricas_columna") & """,""fortalecimiento_cuadriceps"" : """ & i.Item("fortalecimiento_cuadriceps") & """,""fortalecimiento_ligas_inf"" : """ & i.Item("fortalecimiento_ligas_inf") & """,""fortalecimiento_isometricas_inf"" : """ & i.Item("fortalecimiento_isometricas_inf") & """,""fortalecimiento_tensor_fascia_lata"" : """ & i.Item("fortalecimiento_tensor_fascia_lata") & """,""fortalecimiento_isquiotibiales"" : """ & i.Item("fortalecimiento_isquiotibiales") & """,""fortalecimiento_tibial_anterior"" : """ & i.Item("fortalecimiento_tibial_anterior") & """,""fortalecimiento_aductores"" : """ & i.Item("fortalecimiento_aductores") & """,""fortalecimiento_tibial_posterior"" : """ & i.Item("fortalecimiento_tibial_posterior") & """,""fortalecimiento_abductores"" : """ & i.Item("fortalecimiento_abductores") & """,""fortalecimiento_peroneos"" : """ & i.Item("fortalecimiento_peroneos") & """,""fortalecimiento_rotadores_cadera"" : """ & i.Item("fortalecimiento_rotadores_cadera") & """,""fortalecimiento_fascia_plantar"" : """ & i.Item("fortalecimiento_fascia_plantar") & """,""fortalecimiento_polainas_inf"" : """ & i.Item("fortalecimiento_polainas_inf") & """,""fortalecimiento_sin_peso_inf"" : """ & i.Item("fortalecimiento_sin_peso_inf") & """,""fortalecimiento_banco"" : """ & i.Item("fortalecimiento_banco") & """,""fortalecimiento_trampolin"" : """ & i.Item("fortalecimiento_trampolin") & """,""fortalecimiento_bossu"" : """ & i.Item("fortalecimiento_bossu") & """,""fortalecimiento_inferior_3_5"" : """ & i.Item("fortalecimiento_inferior_3_5") & """,""fortalecimiento_inferior_3_10"" : """ & i.Item("fortalecimiento_inferior_3_10") & """,""fortalecimiento_inferior_3_15"" : """ & i.Item("fortalecimiento_inferior_3_15") & """,""reacondicionamiento_bici"" : """ & i.Item("reacondicionamiento_bici") & """,""reacondicionamiento_tiempo_bici"" : """ & i.Item("reacondicionamiento_tiempo_bici") & """,""reacondicionamiento_caminadora"" : """ & i.Item("reacondicionamiento_caminadora") & """,""reacondicionamiento_tiempo_caminadora"" : """ & i.Item("reacondicionamiento_tiempo_caminadora") & """,""reacondicionamiento_reduccion_marcha"" : """ & i.Item("reacondicionamiento_reduccion_marcha") & """,""reacondicionamiento_eliptica"" : """ & i.Item("reacondicionamiento_eliptica") & """,""reacondicionamiento_escaleras"" : """ & i.Item("reacondicionamiento_escaleras") & """,""reacondicionamiento_pelotas"" : """ & i.Item("reacondicionamiento_pelotas") & """,""analgesia_laser_otro"" : """ & i.Item("analgesia_laser_otro") & """,""analgesia_ultrasonido_otro"" : """ & i.Item("analgesia_ultrasonido_otro") & """,""analgesia_electroterapia_otro"" : """ & i.Item("analgesia_electroterapia_otro") & """}"
                    
                Next
                cadena += "]"
                Data = cadena
            Else
                strSql = " select P.idcliente as Id,(P.paterno+' '+P.materno+' '+P.nombre) as paciente,P.edad as edad,p.domicilio as direccion, p.ciudadorigen as origen, CO.descripcion as ocupacion,p.fechanacimiento as fechanacimiento, " & _
       "T.IMedicas as IMedicas, T.CIndicaciones as CIndicaciones,AM.AlegiasPadecimientos as AlegiasPadecimientos, " & _
       "A.Fecha as FechaAgenda, " & _
     "isnull(HT.exploracion_fisica,'') as exploracion_fisica ," & _
     "isnull(HT.exploracion_analgesia,0) as exploracion_analgesia ," & _
     "isnull(HT.exploracion_propiocepsion,0) as exploracion_propiocepsion ," & _
     "isnull(HT.exploracion_desinflamacion,0) as exploracion_desinflamacion ," & _
     "isnull(HT.exploracion_habilidades_manuales,0) as exploracion_habilidades_manuales ," & _
     "isnull(HT.exploracion_fortalecimiento,0) as exploracion_fortalecimiento ," & _
     "isnull(HT.exploracion_aumentar_rangos,0) as exploracion_aumentar_rangos ," & _
     "isnull(HT.exploracion_reduccion_marcha,0) as exploracion_reduccion_marcha ," & _
     "isnull(HT.exploracion_reintegracion_deportiva,0) as exploracion_reintegracion_deportiva ," & _
     "isnull(HT.analgesia_laser,0) as analgesia_laser ," & _
     "isnull(HT.analgesia_ultrasonido,0) as analgesia_ultrasonido ," & _
     "isnull(HT.analgesia_traccion_cervical,0) as analgesia_traccion_cervical ," & _
     "isnull(HT.analgesia_electroterapia,0) as analgesia_electroterapia ," & _
     "isnull(HT.analgesia_masaje,0) as analgesia_masaje ," & _
     "isnull(HT.analgesia_traccion_lumbar,0) as analgesia_traccion_lumbar ," & _
     "isnull(HT.analgesia_tape,0) as analgesia_tape ," & _
     "isnull(HT.analgesia_chc,0) as analgesia_chc ," & _
     "isnull(HT.analgesia_parafina,0) as analgesia_parafina ," & _
     "isnull(HT.analgesia_magneto,0) as analgesia_magneto ," & _
     "isnull(HT.analgesia_crio,0) as analgesia_crio ," & _
     "isnull(HT.analgesia_diatermia,0) as analgesia_diatermia ," & _
     "isnull(HT.analgesia_ondas,0) as analgesia_ondas ," & _
     "isnull(HT.analgesia_hidroterapia,0) as analgesia_hidroterapia ," & _
     "isnull(HT.analgesia_terapia_manual,0) as analgesia_terapia_manual ," & _
     "isnull(HT.analgesia_banios_contraste,0) as analgesia_banios_contraste ," & _
     "isnull(HT.analgesia_cf,0) as analgesia_cf ," & _
     "isnull(HT.analgesia_ejercicio,0) as analgesia_ejercicio ," & _
     "isnull(HT.analgesia_gimnasia,0) as analgesia_gimnasia ," & _
     "isnull(HT.estiramiento_sel,0) as estiramiento_sel ," & _
     "isnull(HT.miembro_superior,0) as miembro_superior ," & _
      "isnull(HT.columna,0) as columna ," & _
      "isnull(HT.miembro_inferior,0) as miembro_inferior ," & _
     "isnull(HT.estiramiento_activo_sup,0) as estiramiento_activo_sup ," & _
     "isnull(HT.estiramiento_pasivo_sup,0) as estiramiento_pasivo_sup ," & _
     "isnull(HT.estiramiento_cintura_escapular,0) as estiramiento_cintura_escapular ," & _
     "isnull(HT.estiramiento_extensores_muneca,0) as estiramiento_extensores_muneca ," & _
     "isnull(HT.estiramiento_extensores_codo,0) as estiramiento_extensores_codo ," & _
     "isnull(HT.estiramiento_flexores_muneca,0) as estiramiento_flexores_muneca ," & _
     "isnull(HT.estiramiento_flexores_codo,0) as estiramiento_flexores_codo ," & _
     "isnull(HT.estiramiento_maq_mov_pasiva_hombro,0) as estiramiento_maq_mov_pasiva_hombro ," & _
     "isnull(HT.estiramiento_mano_hombro,0) as estiramiento_mano_hombro ," & _
     "isnull(HT.estiramiento_superior_3_5,0) as estiramiento_superior_3_5 ," & _
     "isnull(HT.estiramiento_superior_3_10,0) as estiramiento_superior_3_10 ," & _
     "isnull(HT.estiramiento_superior_3_15,0) as estiramiento_superior_3_15 ," & _
     "isnull(HT.estiramiento_dedos,0) as estiramiento_dedos ," & _
     "isnull(HT.estiramiento_otro_superior,'') as estiramiento_otro_superior ," & _
     "isnull(HT.estiramiento_otro_columna,'') as estiramiento_otro_columna ," & _
     "isnull(HT.estiramiento_otro_inferior,'') as estiramiento_otro_inferior ," & _
     "isnull(HT.estiramiento_activo_columna,0) as estiramiento_activo_columna ," & _
     "isnull(HT.estiramiento_pasivo_columna,0) as estiramiento_pasivo_columna ," & _
     "isnull(HT.estiramiento_paravertebrales_dorsales,0) as estiramiento_paravertebrales_dorsales ," & _
     "isnull(HT.estiramiento_paravertebrales_lumbares,0) as estiramiento_paravertebrales_lumbares ," & _
     "isnull(HT.estiramiento_cervicales,0) as estiramiento_cervicales ," & _
     "isnull(HT.estiramiento_paravertebrales,0) as estiramiento_paravertebrales ," & _
     "isnull(HT.estiramiento_columna_3_5,0) as estiramiento_columna_3_5 ," & _
     "isnull(HT.estiramiento_columna_3_10,0) as estiramiento_columna_3_10 ," & _
     "isnull(HT.estiramiento_columna_3_15,0) as estiramiento_columna_3_15 ," & _
     "isnull(HT.estiramiento_trapecio,0) as estiramiento_trapecio ," & _
     "isnull(HT.estiramiento_activo_inf,0) as estiramiento_activo_inf ," & _
     "isnull(HT.estiramiento_pasivo_inf,0) as estiramiento_pasivo_inf ," & _
     "isnull(HT.estiramiento_cuadriceps,0) as estiramiento_cuadriceps ," & _
     "isnull(HT.estiramiento_tensor_fascia_lata,0) as estiramiento_tensor_fascia_lata ," & _
     "isnull(HT.estiramiento_isquiotibiales,0) as estiramiento_isquiotibiales ," & _
     "isnull(HT.estiramiento_tibial_anterior,0) as estiramiento_tibial_anterior ," & _
     "isnull(HT.estiramiento_aductores,0) as estiramiento_aductores ," & _
     "isnull(HT.estiramiento_tibial_posterior,0) as estiramiento_tibial_posterior ," & _
     "isnull(HT.estiramiento_abductores,0) as estiramiento_abductores ," & _
     "isnull(HT.estiramiento_peroneos,0) as estiramiento_peroneos ," & _
     "isnull(HT.estiramiento_rotadores_cadera,0) as estiramiento_rotadores_cadera ," & _
     "isnull(HT.estiramiento_maq_mov_pasiva_rodilla,0) as estiramiento_maq_mov_pasiva_rodilla ," & _
     "isnull(HT.estiramiento_fascia_plantar,0) as estiramiento_fascia_plantar ," & _
     "isnull(HT.estiramiento_maq_mov_pasiva_tobillo,0) as estiramiento_maq_mov_pasiva_tobillo ," & _
     "isnull(HT.estiramiento_inferior_3_5,0) as estiramiento_inferior_3_5 ," & _
     "isnull(HT.estiramiento_inferior_3_10,0) as estiramiento_inferior_3_10 ," & _
     "isnull(HT.estiramiento_inferior_3_15,0) as estiramiento_inferior_3_15 ," & _
     "isnull(HT.fortalecimiento_sel,0) as fortalecimiento_sel ," & _
     "isnull(HT.fortalecimiento_cintura_escapular,0) as fortalecimiento_cintura_escapular ," & _
     "isnull(HT.fortalecimiento_ligas,0) as fortalecimiento_ligas ," & _
     "isnull(HT.fortalecimiento_polainas,0) as fortalecimiento_polainas ," & _
     "isnull(HT.fortalecimiento_isometricas,0) as fortalecimiento_isometricas ," & _
     "isnull(HT.fortalecimiento_sin_peso,0) as fortalecimiento_sin_peso ," & _
     "isnull(HT.fortalecimiento_extensores_muneca,0) as fortalecimiento_extensores_muneca ," & _
     "isnull(HT.fortalecimiento_extensores_codo,0) as fortalecimiento_extensores_codo ," & _
     "isnull(HT.fortalecimiento_flexores_muneca,0) as fortalecimiento_flexores_muneca ," & _
     "isnull(HT.fortalecimiento_flexores_codo,0) as fortalecimiento_flexores_codo ," & _
     "isnull(HT.fortalecimiento_maq_mov_pasiva_hombro,0) as fortalecimiento_maq_mov_pasiva_hombro ," & _
     "isnull(HT.fortalecimiento_mano_hombro,0) as fortalecimiento_mano_hombro ," & _
     "isnull(HT.fortalecimiento_superior_3_5,0) as fortalecimiento_superior_3_5 ," & _
     "isnull(HT.fortalecimiento_superior_3_10,0) as fortalecimiento_superior_3_10 ," & _
     "isnull(HT.fortalecimiento_superior_3_15,0) as fortalecimiento_superior_3_15 ," & _
     "isnull(HT.fortalecimiento_dedos,0) as fortalecimiento_dedos ," & _
     "isnull(HT.fortalecimiento_otro_superior,'') as fortalecimiento_otro_superior ," & _
     "isnull(HT.fortalecimiento_otro_columna,'') as fortalecimiento_otro_columna ," & _
     "isnull(HT.fortalecimiento_otro_inferior,'') as fortalecimiento_otro_inferior ," & _
     "isnull(HT.fortalecimiento_paravertebrales_dorsales,0) as fortalecimiento_paravertebrales_dorsales ," & _
     "isnull(HT.fortalecimiento_williams,0) as fortalecimiento_williams ," & _
     "isnull(HT.fortalecimiento_mckenzic,0) as fortalecimiento_mckenzic ," & _
     "isnull(HT.fortalecimiento_core,0) as fortalecimiento_core ," & _
     "isnull(HT.fortalecimiento_klapp,0) as fortalecimiento_klapp ," & _
     "isnull(HT.fortalecimiento_paravertebrales_lumbares,0) as fortalecimiento_paravertebrales_lumbares ," & _
     "isnull(HT.fortalecimiento_cervicales,0) as fortalecimiento_cervicales ," & _
     "isnull(HT.fortalecimiento_paravertebrales,0) as fortalecimiento_paravertebrales ," & _
     "isnull(HT.fortalecimiento_columna_3_5,0) as fortalecimiento_columna_3_5 ," & _
     "isnull(HT.fortalecimiento_columna_3_10,0) as fortalecimiento_columna_3_10 ," & _
     "isnull(HT.fortalecimiento_columna_3_15,0) as fortalecimiento_columna_3_15 ," & _
     "isnull(HT.fortalecimiento_trapecio,0) as fortalecimiento_trapecio ," & _
     "isnull(HT.fortalecimiento_ligas_columna,0) as fortalecimiento_ligas_columna ," & _
     "isnull(HT.fortalecimiento_isometricas_columna,0) as fortalecimiento_isometricas_columna ," & _
     "isnull(HT.fortalecimiento_cuadriceps,0) as fortalecimiento_cuadriceps ," & _
     "isnull(HT.fortalecimiento_ligas_inf,0) as fortalecimiento_ligas_inf ," & _
     "isnull(HT.fortalecimiento_isometricas_inf,0) as fortalecimiento_isometricas_inf ," & _
     "isnull(HT.fortalecimiento_tensor_fascia_lata,0) as fortalecimiento_tensor_fascia_lata ," & _
     "isnull(HT.fortalecimiento_isquiotibiales,0) as fortalecimiento_isquiotibiales ," & _
     "isnull(HT.fortalecimiento_tibial_anterior,0) as fortalecimiento_tibial_anterior ," & _
     "isnull(HT.fortalecimiento_aductores,0) as fortalecimiento_aductores ," & _
     "isnull(HT.fortalecimiento_tibial_posterior,0) as fortalecimiento_tibial_posterior ," & _
     "isnull(HT.fortalecimiento_abductores,0) as fortalecimiento_abductores ," & _
     "isnull(HT.fortalecimiento_peroneos,0) as fortalecimiento_peroneos ," & _
     "isnull(HT.fortalecimiento_rotadores_cadera,0) as fortalecimiento_rotadores_cadera ," & _
     "isnull(HT.fortalecimiento_fascia_plantar,0) as fortalecimiento_fascia_plantar ," & _
     "isnull(HT.fortalecimiento_polainas_inf,0) as fortalecimiento_polainas_inf ," & _
     "isnull(HT.fortalecimiento_sin_peso_inf,0) as fortalecimiento_sin_peso_inf ," & _
     "isnull(HT.fortalecimiento_banco,0) as fortalecimiento_banco ," & _
     "isnull(HT.fortalecimiento_trampolin,0) as fortalecimiento_trampolin ," & _
     "isnull(HT.fortalecimiento_bossu,0) as fortalecimiento_bossu ," & _
     "isnull(HT.fortalecimiento_inferior_3_5,0) as fortalecimiento_inferior_3_5 ," & _
     "isnull(HT.fortalecimiento_inferior_3_10,0) as fortalecimiento_inferior_3_10 ," & _
     "isnull(HT.fortalecimiento_inferior_3_15,0) as fortalecimiento_inferior_3_15 ," & _
     "isnull(HT.reacondicionamiento_bici,0) as reacondicionamiento_bici ," & _
     "isnull(HT.reacondicionamiento_tiempo_bici,0) as reacondicionamiento_tiempo_bici ," & _
     "isnull(HT.reacondicionamiento_caminadora,0) as reacondicionamiento_caminadora ," & _
     "isnull(HT.reacondicionamiento_tiempo_caminadora,0) as reacondicionamiento_tiempo_caminadora ," & _
     "isnull(HT.reacondicionamiento_reduccion_marcha,0) as reacondicionamiento_reduccion_marcha ," & _
     "isnull(HT.reacondicionamiento_eliptica,0) as reacondicionamiento_eliptica ," & _
     "isnull(HT.reacondicionamiento_escaleras,0) as reacondicionamiento_escaleras ," & _
     "isnull(HT.reacondicionamiento_pelotas,0) as reacondicionamiento_pelotas ," & _
     "isnull(HT.observaciones,'') as observaciones ," & _
     "isnull(HT.analgesia_laser_otro,'') as analgesia_laser_otro ," & _
     "isnull(HT.analgesia_ultrasonido_otro,'') as analgesia_ultrasonido_otro ," & _
     "isnull(HT.analgesia_electroterapia_otro,'') as analgesia_electroterapia_otro ," & _
     "isnull(TP.Nombre,'') as atendio " & _
       "from agenda A " & _
       "inner join clientes P on P.idcliente=A.idcliente " & _
       "left join terapia T on T.idcliente=A.idcliente " & _
       "left join HMTratamientos HT on HT.idcita=A.idcita " & _
       "left join Terapistas TP on TP.id=HT.idatendio " & _
       "left join CatOcupaciones CO on CO.IdOcupacion=P.idocupacion " & _
       "left join HMAntecedentesMedicos AM on AM.idCliente=A.idCliente " & _
       "where A.idCita='" + idcitafecha + "' "
                
                If clsDatos.cargatabla(strSql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                
                        strSql2 = "SELECT A.idcita as idcita,format(A.fecha,'dd/MM/yyyy') as fecha  FROM agenda A " & _
                        " where A.idcliente = '" & dt.Rows(0).Item("Id") & "' AND estado<>'CANCELADO' and convert(varchar(10),((select fecha from agenda where Idcita = '" + idcita + "')),103)>=A.fecha " & _
                        " order by A.fecha desc "
                        If clsDatos.cargatabla(strSql2, dt2) = 0 Then
                            If dt.Rows.Count > 0 Then
                                Dim FechaA As String
                                Dim IdcitaA As String
                                Dim count2 As Integer = 1
                                For Each F As DataRow In dt2.Rows
                                    If count2 > 1 Then
                                        FechaA += "."
                                        IdcitaA += "."
                                    End If
                                    count2 = count2 + 1
                                    If Not IsDBNull(F.Item("fecha")) Then
                                        FechaA += F.Item("fecha")
                                    End If
                                    If Not IsDBNull(F.Item("idcita")) Then
                                        IdcitaA += F.Item("idcita").ToString
                                    End If
                            
                            
                            
                                Next
                                Fechas = FechaA
                                icita = IdcitaA
                            End If
                        Else
                            'FechaA
                        End If
                
                
                        strSql3 = "SELECT id, Nombre  FROM Terapistas T where turno='" + turno + "' " & _
                      " order by id Asc "
                        If clsDatos.cargatabla(strSql3, dt3) = 0 Then
                            If dt.Rows.Count > 0 Then
                                Dim Terapista As String
                                Dim IdTerapista As String
                                Dim countT As Integer = 1
                                For Each T As DataRow In dt3.Rows
                                    If countT > 1 Then
                                        Terapista += "."
                                        IdTerapista += "."
                                    End If
                                    countT = countT + 1
                                    If Not IsDBNull(T.Item("Nombre")) Then
                                        Terapista += T.Item("Nombre")
                                    End If
                                    If Not IsDBNull(T.Item("id")) Then
                                        IdTerapista += T.Item("id").ToString
                                    End If
                            
                            
                            
                                Next
                                CTerapista = Terapista
                                CidTerapista = IdTerapista
                            End If
                        Else
                            'TERAPISTA
                        End If
                
                        Dim cadena As String
                        Dim count As Integer = 1
                        cadena = "["
                        For Each i As DataRow In dt.Rows
                            If count > 1 Then
                                cadena += ","
                            End If
                            count = count + 1
                            'cadena += "{""id"":""" & i.Item("Id") & """,""nombre"":""" & i.Item("paciente") & """,""fecha_nac"":""" & i.Item("fechanacimiento") & """,""edad"":""" & i.Item("edad") & """,""origen"":""" & i.Item("origen") & """,""ocupacion"":""" & i.Item("ocupacion") & """,""direccion"":""" & i.Item("direccion") & """,""alergias"":""" & i.Item("AlegiasPadecimientos") & """, ""indicaciones_medicas"":""" & i.Item("IMedicas") & """, ""contra_indicaciones"":""" & i.Item("CIndicaciones") & """ , ""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""ultrasonido"":""" & i.Item("ultrasonido") & """, ""chc"":""" & i.Item("chc") & """, ""electroterapia"":""" & i.Item("electroterapia") & """, ""laser"":""" & i.Item("laser") & """, ""cf"":""" & i.Item("cf") & """, ""ejercicio"":""" & i.Item("ejercicio") & """,""masaje"":""" & i.Item("masaje") & """,""magneto"":""" & i.Item("magneto") & """,""gimnasia"":""" & i.Item("gimnasia") & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """}"
                            cadena += "{""id"":""" & i.Item("Id") & """,""nombre"":""" & i.Item("paciente") & """,""fecha_nac"":""" & i.Item("fechanacimiento") & """,""edad"":""" & i.Item("edad") & """,""origen"":""" & i.Item("origen") & """,""ocupacion"":""" & i.Item("ocupacion") & """,""direccion"":""" & i.Item("direccion") & """,""alergias"":""" & i.Item("AlegiasPadecimientos") & """, ""indicaciones_medicas"":""" & i.Item("IMedicas") & """, ""contra_indicaciones"":""" & i.Item("CIndicaciones") & """ , ""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """,""exploracion_fisica"" : """ & i.Item("exploracion_fisica") & """,""exploracion_analgesia"" : """ & i.Item("exploracion_analgesia") & """,""exploracion_propiocepsion"" : """ & i.Item("exploracion_propiocepsion") & """,""exploracion_desinflamacion"" : """ & i.Item("exploracion_desinflamacion") & """,""exploracion_habilidades_manuales"" : """ & i.Item("exploracion_habilidades_manuales") & """,""exploracion_fortalecimiento"" : """ & i.Item("exploracion_fortalecimiento") & """,""exploracion_aumentar_rangos"" : """ & i.Item("exploracion_aumentar_rangos") & """,""exploracion_reduccion_marcha"" : """ & i.Item("exploracion_reduccion_marcha") & """,""exploracion_reintegracion_deportiva"" : """ & i.Item("exploracion_reintegracion_deportiva") & """,""analgesia_laser"" : """ & i.Item("analgesia_laser") & """,""analgesia_ultrasonido"" : """ & i.Item("analgesia_ultrasonido") & """,""analgesia_traccion_cervical"" : """ & i.Item("analgesia_traccion_cervical") & """,""analgesia_electroterapia"" : """ & i.Item("analgesia_electroterapia") & """,""analgesia_masaje"" : """ & i.Item("analgesia_masaje") & """,""analgesia_traccion_lumbar"" : """ & i.Item("analgesia_traccion_lumbar") & """,""analgesia_tape"" : """ & i.Item("analgesia_tape") & """,""analgesia_chc"" : """ & i.Item("analgesia_chc") & """,""analgesia_parafina"" : """ & i.Item("analgesia_parafina") & """,""analgesia_magneto"" : """ & i.Item("analgesia_magneto") & """,""analgesia_crio"" : """ & i.Item("analgesia_crio") & """,""analgesia_diatermia"" : """ & i.Item("analgesia_diatermia") & """,""analgesia_ondas"" : """ & i.Item("analgesia_ondas") & """,""analgesia_hidroterapia"" : """ & i.Item("analgesia_hidroterapia") & """,""analgesia_terapia_manual"" : """ & i.Item("analgesia_terapia_manual") & """,""analgesia_banios_contraste"" : """ & i.Item("analgesia_banios_contraste") & """,""analgesia_cf"" : """ & i.Item("analgesia_cf") & """,""analgesia_ejercicio"" : """ & i.Item("analgesia_ejercicio") & """,""analgesia_gimnasia"" : """ & i.Item("analgesia_gimnasia") & """,""estiramiento_sel"" : """ & i.Item("estiramiento_sel") & """,""miembro_superior"" : """ & i.Item("miembro_superior") & """,""columna"" : """ & i.Item("columna") & """,""miembro_inferior"" : """ & i.Item("miembro_inferior") & """,""estiramiento_activo_sup"" : """ & i.Item("estiramiento_activo_sup") & """,""estiramiento_pasivo_sup"" : """ & i.Item("estiramiento_pasivo_sup") & """,""estiramiento_cintura_escapular"" : """ & i.Item("estiramiento_cintura_escapular") & """,""estiramiento_extensores_muneca"" : """ & i.Item("estiramiento_extensores_muneca") & """,""estiramiento_extensores_codo"" : """ & i.Item("estiramiento_extensores_codo") & """,""estiramiento_flexores_muneca"" : """ & i.Item("estiramiento_flexores_muneca") & """,""estiramiento_flexores_codo"" : """ & i.Item("estiramiento_flexores_codo") & """,""estiramiento_maq_mov_pasiva_hombro"" : """ & i.Item("estiramiento_maq_mov_pasiva_hombro") & """,""estiramiento_mano_hombro"" : """ & i.Item("estiramiento_mano_hombro") & """,""estiramiento_superior_3_5"" : """ & i.Item("estiramiento_superior_3_5") & """,""estiramiento_superior_3_10"" : """ & i.Item("estiramiento_superior_3_10") & """,""estiramiento_superior_3_15"" : """ & i.Item("estiramiento_superior_3_15") & """,""estiramiento_dedos"" : """ & i.Item("estiramiento_dedos") & """,""estiramiento_otro_superior"" : """ & i.Item("estiramiento_otro_superior") & """,""estiramiento_otro_columna"" : """ & i.Item("estiramiento_otro_columna") & """,""estiramiento_otro_inferior"" : """ & i.Item("estiramiento_otro_inferior") & """,""estiramiento_activo_columna"" : """ & i.Item("estiramiento_activo_columna") & """,""estiramiento_pasivo_columna"" : """ & i.Item("estiramiento_pasivo_columna") & """,""estiramiento_paravertebrales_dorsales"" : """ & i.Item("estiramiento_paravertebrales_dorsales") & """,""estiramiento_paravertebrales_lumbares"" : """ & i.Item("estiramiento_paravertebrales_lumbares") & """,""estiramiento_cervicales"" : """ & i.Item("estiramiento_cervicales") & """,""estiramiento_paravertebrales"" : """ & i.Item("estiramiento_paravertebrales") & """,""estiramiento_columna_3_5"" : """ & i.Item("estiramiento_columna_3_5") & """,""estiramiento_columna_3_10"" : """ & i.Item("estiramiento_columna_3_10") & """,""estiramiento_columna_3_15"" : """ & i.Item("estiramiento_columna_3_15") & """,""estiramiento_trapecio"" : """ & i.Item("estiramiento_trapecio") & """,""estiramiento_activo_inf"" : """ & i.Item("estiramiento_activo_inf") & """,""estiramiento_pasivo_inf"" : """ & i.Item("estiramiento_pasivo_inf") & """,""estiramiento_cuadriceps"" : """ & i.Item("estiramiento_cuadriceps") & """,""estiramiento_tensor_fascia_lata"" : """ & i.Item("estiramiento_tensor_fascia_lata") & """,""estiramiento_isquiotibiales"" : """ & i.Item("estiramiento_isquiotibiales") & """,""estiramiento_tibial_anterior"" : """ & i.Item("estiramiento_tibial_anterior") & """,""estiramiento_aductores"" : """ & i.Item("estiramiento_aductores") & """,""estiramiento_tibial_posterior"" : """ & i.Item("estiramiento_tibial_posterior") & """,""estiramiento_abductores"" : """ & i.Item("estiramiento_abductores") & """,""estiramiento_peroneos"" : """ & i.Item("estiramiento_peroneos") & """,""estiramiento_rotadores_cadera"" : """ & i.Item("estiramiento_rotadores_cadera") & """,""estiramiento_maq_mov_pasiva_rodilla"" : """ & i.Item("estiramiento_maq_mov_pasiva_rodilla") & """,""estiramiento_fascia_plantar"" : """ & i.Item("estiramiento_fascia_plantar") & """,""estiramiento_maq_mov_pasiva_tobillo"" : """ & i.Item("estiramiento_maq_mov_pasiva_tobillo") & """,""estiramiento_inferior_3_5"" : """ & i.Item("estiramiento_inferior_3_5") & """,""estiramiento_inferior_3_10"" : """ & i.Item("estiramiento_inferior_3_10") & """,""estiramiento_inferior_3_15"" : """ & i.Item("estiramiento_inferior_3_15") & """,""fortalecimiento_sel"" : """ & i.Item("fortalecimiento_sel") & """,""fortalecimiento_cintura_escapular"" : """ & i.Item("fortalecimiento_cintura_escapular") & """,""fortalecimiento_ligas"" : """ & i.Item("fortalecimiento_ligas") & """,""fortalecimiento_polainas"" : """ & i.Item("fortalecimiento_polainas") & """,""fortalecimiento_isometricas"" : """ & i.Item("fortalecimiento_isometricas") & """,""fortalecimiento_sin_peso"" : """ & i.Item("fortalecimiento_sin_peso") & """,""fortalecimiento_extensores_muneca"" : """ & i.Item("fortalecimiento_extensores_muneca") & """,""fortalecimiento_extensores_codo"" : """ & i.Item("fortalecimiento_extensores_codo") & """,""fortalecimiento_flexores_muneca"" : """ & i.Item("fortalecimiento_flexores_muneca") & """,""fortalecimiento_flexores_codo"" : """ & i.Item("fortalecimiento_flexores_codo") & """,""fortalecimiento_maq_mov_pasiva_hombro"" : """ & i.Item("fortalecimiento_maq_mov_pasiva_hombro") & """,""fortalecimiento_mano_hombro"" : """ & i.Item("fortalecimiento_mano_hombro") & """,""fortalecimiento_superior_3_5"" : """ & i.Item("fortalecimiento_superior_3_5") & """,""fortalecimiento_superior_3_10"" : """ & i.Item("fortalecimiento_superior_3_10") & """,""fortalecimiento_superior_3_15"" : """ & i.Item("fortalecimiento_superior_3_15") & """,""fortalecimiento_dedos"" : """ & i.Item("fortalecimiento_dedos") & """,""fortalecimiento_otro_superior"" : """ & i.Item("fortalecimiento_otro_superior") & """,""fortalecimiento_otro_columna"" : """ & i.Item("fortalecimiento_otro_columna") & """,""fortalecimiento_otro_inferior"" : """ & i.Item("fortalecimiento_otro_inferior") & """,""fortalecimiento_paravertebrales_dorsales"" : """ & i.Item("fortalecimiento_paravertebrales_dorsales") & """,""fortalecimiento_williams"" : """ & i.Item("fortalecimiento_williams") & """,""fortalecimiento_mckenzic"" : """ & i.Item("fortalecimiento_mckenzic") & """,""fortalecimiento_core"" : """ & i.Item("fortalecimiento_core") & """,""fortalecimiento_klapp"" : """ & i.Item("fortalecimiento_klapp") & """,""fortalecimiento_paravertebrales_lumbares"" : """ & i.Item("fortalecimiento_paravertebrales_lumbares") & """,""fortalecimiento_cervicales"" : """ & i.Item("fortalecimiento_cervicales") & """,""fortalecimiento_paravertebrales"" : """ & i.Item("fortalecimiento_paravertebrales") & """,""fortalecimiento_columna_3_5"" : """ & i.Item("fortalecimiento_columna_3_5") & """,""fortalecimiento_columna_3_10"" : """ & i.Item("fortalecimiento_columna_3_10") & """,""fortalecimiento_columna_3_15"" : """ & i.Item("fortalecimiento_columna_3_15") & """,""fortalecimiento_trapecio"" : """ & i.Item("fortalecimiento_trapecio") & """,""fortalecimiento_ligas_columna"" : """ & i.Item("fortalecimiento_ligas_columna") & """,""fortalecimiento_isometricas_columna"" : """ & i.Item("fortalecimiento_isometricas_columna") & """,""fortalecimiento_cuadriceps"" : """ & i.Item("fortalecimiento_cuadriceps") & """,""fortalecimiento_ligas_inf"" : """ & i.Item("fortalecimiento_ligas_inf") & """,""fortalecimiento_isometricas_inf"" : """ & i.Item("fortalecimiento_isometricas_inf") & """,""fortalecimiento_tensor_fascia_lata"" : """ & i.Item("fortalecimiento_tensor_fascia_lata") & """,""fortalecimiento_isquiotibiales"" : """ & i.Item("fortalecimiento_isquiotibiales") & """,""fortalecimiento_tibial_anterior"" : """ & i.Item("fortalecimiento_tibial_anterior") & """,""fortalecimiento_aductores"" : """ & i.Item("fortalecimiento_aductores") & """,""fortalecimiento_tibial_posterior"" : """ & i.Item("fortalecimiento_tibial_posterior") & """,""fortalecimiento_abductores"" : """ & i.Item("fortalecimiento_abductores") & """,""fortalecimiento_peroneos"" : """ & i.Item("fortalecimiento_peroneos") & """,""fortalecimiento_rotadores_cadera"" : """ & i.Item("fortalecimiento_rotadores_cadera") & """,""fortalecimiento_fascia_plantar"" : """ & i.Item("fortalecimiento_fascia_plantar") & """,""fortalecimiento_polainas_inf"" : """ & i.Item("fortalecimiento_polainas_inf") & """,""fortalecimiento_sin_peso_inf"" : """ & i.Item("fortalecimiento_sin_peso_inf") & """,""fortalecimiento_banco"" : """ & i.Item("fortalecimiento_banco") & """,""fortalecimiento_trampolin"" : """ & i.Item("fortalecimiento_trampolin") & """,""fortalecimiento_bossu"" : """ & i.Item("fortalecimiento_bossu") & """,""fortalecimiento_inferior_3_5"" : """ & i.Item("fortalecimiento_inferior_3_5") & """,""fortalecimiento_inferior_3_10"" : """ & i.Item("fortalecimiento_inferior_3_10") & """,""fortalecimiento_inferior_3_15"" : """ & i.Item("fortalecimiento_inferior_3_15") & """,""reacondicionamiento_bici"" : """ & i.Item("reacondicionamiento_bici") & """,""reacondicionamiento_tiempo_bici"" : """ & i.Item("reacondicionamiento_tiempo_bici") & """,""reacondicionamiento_caminadora"" : """ & i.Item("reacondicionamiento_caminadora") & """,""reacondicionamiento_tiempo_caminadora"" : """ & i.Item("reacondicionamiento_tiempo_caminadora") & """,""reacondicionamiento_reduccion_marcha"" : """ & i.Item("reacondicionamiento_reduccion_marcha") & """,""reacondicionamiento_eliptica"" : """ & i.Item("reacondicionamiento_eliptica") & """,""reacondicionamiento_escaleras"" : """ & i.Item("reacondicionamiento_escaleras") & """,""reacondicionamiento_pelotas"" : """ & i.Item("reacondicionamiento_pelotas") & """,""analgesia_laser_otro"" : """ & i.Item("analgesia_laser_otro") & """,""analgesia_ultrasonido_otro"" : """ & i.Item("analgesia_ultrasonido_otro") & """,""analgesia_electroterapia_otro"" : """ & i.Item("analgesia_electroterapia_otro") & """}"
                            'cadena += "{""id"":""" & i.Item("Id") & """,""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """,""exploracion_analgesia"" : """ & i.Item("exploracion_analgesia") & """,""exploracion_propiocepsion"" : """ & i.Item("exploracion_propiocepsion") & """,""exploracion_desinflamacion"" : """ & i.Item("exploracion_desinflamacion") & """,""exploracion_habilidades_manuales"" : """ & i.Item("exploracion_habilidades_manuales") & """,""exploracion_fortalecimiento"" : """ & i.Item("exploracion_fortalecimiento") & """,""exploracion_aumentar_rangos"" : """ & i.Item("exploracion_aumentar_rangos") & """,""exploracion_reduccion_marcha"" : """ & i.Item("exploracion_reduccion_marcha") & """,""exploracion_reintegracion_deportiva"" : """ & i.Item("exploracion_reintegracion_deportiva") & """,""analgesia_laser"" : """ & i.Item("analgesia_laser") & """,""analgesia_ultrasonido"" : """ & i.Item("analgesia_ultrasonido") & """,""analgesia_traccion_cervical"" : """ & i.Item("analgesia_traccion_cervical") & """,""analgesia_electroterapia"" : """ & i.Item("analgesia_electroterapia") & """,""analgesia_masaje"" : """ & i.Item("analgesia_masaje") & """,""analgesia_traccion_lumbar"" : """ & i.Item("analgesia_traccion_lumbar") & """,""analgesia_tape"" : """ & i.Item("analgesia_tape") & """,""analgesia_chc"" : """ & i.Item("analgesia_chc") & """,""analgesia_parafina"" : """ & i.Item("analgesia_parafina") & """,""analgesia_magneto"" : """ & i.Item("analgesia_magneto") & """,""analgesia_crio"" : """ & i.Item("analgesia_crio") & """,""analgesia_diatermia"" : """ & i.Item("analgesia_diatermia") & """,""analgesia_ondas"" : """ & i.Item("analgesia_ondas") & """,""analgesia_hidroterapia"" : """ & i.Item("analgesia_hidroterapia") & """,""analgesia_terapia_manual"" : """ & i.Item("analgesia_terapia_manual") & """,""analgesia_banios_contraste"" : """ & i.Item("analgesia_banios_contraste") & """,""analgesia_cf"" : """ & i.Item("analgesia_cf") & """,""analgesia_ejercicio"" : """ & i.Item("analgesia_ejercicio") & """,""analgesia_gimnasia"" : """ & i.Item("analgesia_gimnasia") & """,""estiramiento_sel"" : """ & i.Item("estiramiento_sel") & """,""estiramiento_activo_sup"" : """ & i.Item("estiramiento_activo_sup") & """,""estiramiento_pasivo_sup"" : """ & i.Item("estiramiento_pasivo_sup") & """,""estiramiento_cintura_escapular"" : """ & i.Item("estiramiento_cintura_escapular") & """,""estiramiento_extensores_muneca"" : """ & i.Item("estiramiento_extensores_muneca") & """,""estiramiento_extensores_codo"" : """ & i.Item("estiramiento_extensores_codo") & """,""estiramiento_flexores_muneca"" : """ & i.Item("estiramiento_flexores_muneca") & """,""estiramiento_flexores_codo"" : """ & i.Item("estiramiento_flexores_codo") & """,""estiramiento_maq_mov_pasiva_hombro"" : """ & i.Item("estiramiento_maq_mov_pasiva_hombro") & """,""estiramiento_mano_hombro"" : """ & i.Item("estiramiento_mano_hombro") & """,""estiramiento_superior_3_5"" : """ & i.Item("estiramiento_superior_3_5") & """,""estiramiento_superior_3_10"" : """ & i.Item("estiramiento_superior_3_10") & """,""estiramiento_superior_3_15"" : """ & i.Item("estiramiento_superior_3_15") & """,""estiramiento_dedos"" : """ & i.Item("estiramiento_dedos") & """,""estiramiento_otro_estiramiento"" : """ & i.Item("estiramiento_otro_estiramiento") & """,""estiramiento_activo_columna"" : """ & i.Item("estiramiento_activo_columna") & """,""estiramiento_pasivo_columna"" : """ & i.Item("estiramiento_pasivo_columna") & """,""estiramiento_paravertebrales_dorsales"" : """ & i.Item("estiramiento_paravertebrales_dorsales") & """,""estiramiento_paravertebrales_lumbares"" : """ & i.Item("estiramiento_paravertebrales_lumbares") & """,""estiramiento_cervicales"" : """ & i.Item("estiramiento_cervicales") & """,""estiramiento_paravertebrales"" : """ & i.Item("estiramiento_paravertebrales") & """,""estiramiento_columna_3_5"" : """ & i.Item("estiramiento_columna_3_5") & """,""estiramiento_columna_3_10"" : """ & i.Item("estiramiento_columna_3_10") & """,""estiramiento_columna_3_15"" : """ & i.Item("estiramiento_columna_3_15") & """,""estiramiento_trapecio"" : """ & i.Item("estiramiento_trapecio") & """,""estiramiento_activo_inf"" : """ & i.Item("estiramiento_activo_inf") & """,""estiramiento_pasivo_inf"" : """ & i.Item("estiramiento_pasivo_inf") & """,""estiramiento_cuadriceps"" : """ & i.Item("estiramiento_cuadriceps") & """,""estiramiento_tensor_fascia_lata"" : """ & i.Item("estiramiento_tensor_fascia_lata") & """,""estiramiento_isquiotibiales"" : """ & i.Item("estiramiento_isquiotibiales") & """,""estiramiento_tibial_anterior"" : """ & i.Item("estiramiento_tibial_anterior") & """,""estiramiento_aductores"" : """ & i.Item("estiramiento_aductores") & """,""estiramiento_tibial_posterior"" : """ & i.Item("estiramiento_tibial_posterior") & """,""estiramiento_abductores"" : """ & i.Item("estiramiento_abductores") & """,""estiramiento_peroneos"" : """ & i.Item("estiramiento_peroneos") & """,""estiramiento_rotadores_cadera"" : """ & i.Item("estiramiento_rotadores_cadera") & """,""estiramiento_maq_mov_pasiva_rodilla"" : """ & i.Item("estiramiento_maq_mov_pasiva_rodilla") & """,""estiramiento_fascia_plantar"" : """ & i.Item("estiramiento_fascia_plantar") & """,""estiramiento_maq_mov_pasiva_tobillo"" : """ & i.Item("estiramiento_maq_mov_pasiva_tobillo") & """,""estiramiento_inferior_3_5"" : """ & i.Item("estiramiento_inferior_3_5") & """,""estiramiento_inferior_3_10"" : """ & i.Item("estiramiento_inferior_3_10") & """,""estiramiento_inferior_3_15"" : """ & i.Item("estiramiento_inferior_3_15") & """,""fortalecimiento_sel"" : """ & i.Item("fortalecimiento_sel") & """,""fortalecimiento_cintura_escapular"" : """ & i.Item("fortalecimiento_cintura_escapular") & """,""fortalecimiento_ligas"" : """ & i.Item("fortalecimiento_ligas") & """,""fortalecimiento_polainas"" : """ & i.Item("fortalecimiento_polainas") & """,""fortalecimiento_isometricas"" : """ & i.Item("fortalecimiento_isometricas") & """,""fortalecimiento_sin_peso"" : """ & i.Item("fortalecimiento_sin_peso") & """,""fortalecimiento_extensores_muneca"" : """ & i.Item("fortalecimiento_extensores_muneca") & """,""fortalecimiento_extensores_codo"" : """ & i.Item("fortalecimiento_extensores_codo") & """,""fortalecimiento_flexores_muneca"" : """ & i.Item("fortalecimiento_flexores_muneca") & """,""fortalecimiento_flexores_codo"" : """ & i.Item("fortalecimiento_flexores_codo") & """,""fortalecimiento_maq_mov_pasiva_hombro"" : """ & i.Item("fortalecimiento_maq_mov_pasiva_hombro") & """,""fortalecimiento_mano_hombro"" : """ & i.Item("fortalecimiento_mano_hombro") & """,""fortalecimiento_superior_3_5"" : """ & i.Item("fortalecimiento_superior_3_5") & """,""fortalecimiento_superior_3_10"" : """ & i.Item("fortalecimiento_superior_3_10") & """,""fortalecimiento_superior_3_15"" : """ & i.Item("fortalecimiento_superior_3_15") & """,""fortalecimiento_dedos"" : """ & i.Item("fortalecimiento_dedos") & """,""fortalecimiento_otro_estiramiento"" : """ & i.Item("fortalecimiento_otro_estiramiento") & """,""fortalecimiento_paravertebrales_dorsales"" : """ & i.Item("fortalecimiento_paravertebrales_dorsales") & """,""fortalecimiento_williams"" : """ & i.Item("fortalecimiento_williams") & """,""fortalecimiento_mckenzic"" : """ & i.Item("fortalecimiento_mckenzic") & """,""fortalecimiento_core"" : """ & i.Item("fortalecimiento_core") & """,""fortalecimiento_klapp"" : """ & i.Item("fortalecimiento_klapp") & """,""fortalecimiento_paravertebrales_lumbares"" : """ & i.Item("fortalecimiento_paravertebrales_lumbares") & """,""fortalecimiento_cervicales"" : """ & i.Item("fortalecimiento_cervicales") & """,""fortalecimiento_paravertebrales"" : """ & i.Item("fortalecimiento_paravertebrales") & """,""fortalecimiento_columna_3_5"" : """ & i.Item("fortalecimiento_columna_3_5") & """,""fortalecimiento_columna_3_10"" : """ & i.Item("fortalecimiento_columna_3_10") & """,""fortalecimiento_columna_3_15"" : """ & i.Item("fortalecimiento_columna_3_15") & """,""fortalecimiento_trapecio"" : """ & i.Item("fortalecimiento_trapecio") & """,""fortalecimiento_ligas_columna"" : """ & i.Item("fortalecimiento_ligas_columna") & """,""fortalecimiento_isometricas_columna"" : """ & i.Item("fortalecimiento_isometricas_columna") & """,""fortalecimiento_cuadriceps"" : """ & i.Item("fortalecimiento_cuadriceps") & """,""fortalecimiento_ligas_inf"" : """ & i.Item("fortalecimiento_ligas_inf") & """,""fortalecimiento_isometricas_inf"" : """ & i.Item("fortalecimiento_isometricas_inf") & """,""fortalecimiento_tensor_fascia_lata"" : """ & i.Item("fortalecimiento_tensor_fascia_lata") & """,""fortalecimiento_isquiotibiales"" : """ & i.Item("fortalecimiento_isquiotibiales") & """,""fortalecimiento_tibial_anterior"" : """ & i.Item("fortalecimiento_tibial_anterior") & """,""fortalecimiento_aductores"" : """ & i.Item("fortalecimiento_aductores") & """,""fortalecimiento_tibial_posterior"" : """ & i.Item("fortalecimiento_tibial_posterior") & """,""fortalecimiento_abductores"" : """ & i.Item("fortalecimiento_abductores") & """,""fortalecimiento_peroneos"" : """ & i.Item("fortalecimiento_peroneos") & """,""fortalecimiento_rotadores_cadera"" : """ & i.Item("fortalecimiento_rotadores_cadera") & """,""fortalecimiento_fascia_plantar"" : """ & i.Item("fortalecimiento_fascia_plantar") & """,""fortalecimiento_polainas_inf"" : """ & i.Item("fortalecimiento_polainas_inf") & """,""fortalecimiento_sin_peso_inf"" : """ & i.Item("fortalecimiento_sin_peso_inf") & """,""fortalecimiento_banco"" : """ & i.Item("fortalecimiento_banco") & """,""fortalecimiento_trampolin"" : """ & i.Item("fortalecimiento_trampolin") & """,""fortalecimiento_bossu"" : """ & i.Item("fortalecimiento_bossu") & """,""fortalecimiento_inferior_3_5"" : """ & i.Item("fortalecimiento_inferior_3_5") & """,""fortalecimiento_inferior_3_10"" : """ & i.Item("fortalecimiento_inferior_3_10") & """,""fortalecimiento_inferior_3_15"" : """ & i.Item("fortalecimiento_inferior_3_15") & """,""reacondicionamiento_bici"" : """ & i.Item("reacondicionamiento_bici") & """,""reacondicionamiento_tiempo_bici"" : """ & i.Item("reacondicionamiento_tiempo_bici") & """,""reacondicionamiento_caminadora"" : """ & i.Item("reacondicionamiento_caminadora") & """,""reacondicionamiento_tiempo_caminadora"" : """ & i.Item("reacondicionamiento_tiempo_caminadora") & """,""reacondicionamiento_reduccion_marcha"" : """ & i.Item("reacondicionamiento_reduccion_marcha") & """,""reacondicionamiento_eliptica"" : """ & i.Item("reacondicionamiento_eliptica") & """,""reacondicionamiento_escaleras"" : """ & i.Item("reacondicionamiento_escaleras") & """,""reacondicionamiento_pelotas"" : """ & i.Item("reacondicionamiento_pelotas") & """}"
                    
                        Next
                        cadena += "]"
                        Data = cadena

                    End If
                Else
                    'data = "[{""id"":""1"",""nombre"":""Christhian Froilan Sosa Cebalos"",""fecha_nac"":""24/04/1981"",""edad"":""37"",""origen"":""Mérida"",""ocupacion"":""Máster de la web"",""direccion"":""Vergel III"",""alergias"":""a los hombres"", ""indicaciones_medicas"":""Tomar viagra"", ""contra_indicaciones"":""No tomar viagra si tomó alcohol"",""id_diagnosticos"":""1.2.3"", ""diagnosticos"":""HOMBRO CONDROMATOSIS.HOMBRO.TOBILLO"", ""protocolo"":""3"",""fase"":""4"", ""fecha"":""20/02/2019.24/02/2019.28/02/2019"",""ultrasonido"":""1"", ""chc"":""1"", ""electroterapia"":""0"", ""laser"":""1"", ""cf"":""1"", ""ejercicio"":""1"",""masaje"":""0"",""magneto"":""0"",""gimnasia"":""0"",""observaciones"":""Esta muy guapo el paciente"" }]"
                End If
                ' Return Data
                
                '  strSql2 = "SELECT A.idcita as idcita,format(A.fecha,'dd/MM/yyyy') as fecha  FROM agenda A " & _
                '  " where A.idcliente = '" & idcliente & "' AND estado<>'CANCELADO' and convert(varchar(10),((select fecha from agenda where Idcita = '" + idcita + "')),103)>=A.fecha " & _
                '  " order by A.fecha desc "
                '  If clsDatos.cargatabla(strSql2, dt2) = 0 Then
                '      If dt.Rows.Count > 0 Then
                '          Dim FechaA As String
                '          Dim IdcitaA As String
                '          Dim count2 As Integer = 1
                '          For Each F As DataRow In dt2.Rows
                '              If count2 > 1 Then
                '                  FechaA += "."
                '                  IdcitaA += "."
                '              End If
                '              count2 = count2 + 1
                '              If Not IsDBNull(F.Item("fecha")) Then
                '                  FechaA += F.Item("fecha")
                '              End If
                '              If Not IsDBNull(F.Item("idcita")) Then
                '                  IdcitaA += F.Item("idcita").ToString
                '              End If
                            
                            
                            
                '          Next
                '          Fechas = FechaA
                '          icita = IdcitaA
                '      End If
                '  Else
                '      'FechaA
                '  End If
                
                '  strSql3 = "SELECT id, Nombre  FROM Terapistas T where turno='" + turno + "' " & _
                '" order by id Asc "
                '  If clsDatos.cargatabla(strSql3, dt3) = 0 Then
                '      If dt.Rows.Count > 0 Then
                '          Dim Terapista As String
                '          Dim IdTerapista As String
                '          Dim countT As Integer = 1
                '          For Each T As DataRow In dt3.Rows
                '              If countT > 1 Then
                '                  Terapista += "."
                '                  IdTerapista += "."
                '              End If
                '              countT = countT + 1
                '              If Not IsDBNull(T.Item("Nombre")) Then
                '                  Terapista += T.Item("Nombre")
                '              End If
                '              If Not IsDBNull(T.Item("id")) Then
                '                  IdTerapista += T.Item("id").ToString
                '              End If
                            
                            
                            
                '          Next
                '          CTerapista = Terapista
                '          CidTerapista = IdTerapista
                '      End If
                '  Else
                '      'TERAPISTA
                '  End If
                '  Dim cadena As String
                '  Dim count As Integer = 1
                '  cadena = "["
                
                '  'cadena += "{""id"":""" & i.Item("idtratamiento") & """, ""ultrasonido"":""" & i.Item("ultrasonido") & """, ""chc"":""" & i.Item("chc") & """, ""electroterapia"":""" & i.Item("electroterapia") & """, ""laser"":""" & i.Item("laser") & """, ""cf"":""" & i.Item("cf") & """, ""ejercicio"":""" & i.Item("ejercicio") & """,""masaje"":""" & i.Item("masaje") & """,""magneto"":""" & i.Item("magneto") & """,""gimnasia"":""" & i.Item("gimnasia") & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """}"
                '  cadena += "{""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """}"
               
                '  cadena += "]"
                '  Data = cadena
                '  'cadena += "{""id"":""" & i.Item("idtratamiento") & """,""observaciones"":""" & i.Item("observaciones") & """,""atendio"":""" & i.Item("atendio") & """,""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""terapista"":""" & CTerapista & """, ""idterapista"":""" & CidTerapista & """,""exploracion_analgesia"" : """ & i.Item("exploracion_analgesia") & """,""exploracion_propiocepsion"" : """ & i.Item("exploracion_propiocepsion") & """,""exploracion_desinflamacion"" : """ & i.Item("exploracion_desinflamacion") & """,""exploracion_habilidades_manuales"" : """ & i.Item("exploracion_habilidades_manuales") & """,""exploracion_fortalecimiento"" : """ & i.Item("exploracion_fortalecimiento") & """,""exploracion_aumentar_rangos"" : """ & i.Item("exploracion_aumentar_rangos") & """,""exploracion_reduccion_marcha"" : """ & i.Item("exploracion_reduccion_marcha") & """,""exploracion_reintegracion_deportiva"" : """ & i.Item("exploracion_reintegracion_deportiva") & """,""analgesia_laser"" : """ & i.Item("analgesia_laser") & """,""analgesia_ultrasonido"" : """ & i.Item("analgesia_ultrasonido") & """,""analgesia_traccion_cervical"" : """ & i.Item("analgesia_traccion_cervical") & """,""analgesia_electroterapia"" : """ & i.Item("analgesia_electroterapia") & """,""analgesia_masaje"" : """ & i.Item("analgesia_masaje") & """,""analgesia_traccion_lumbar"" : """ & i.Item("analgesia_traccion_lumbar") & """,""analgesia_tape"" : """ & i.Item("analgesia_tape") & """,""analgesia_chc"" : """ & i.Item("analgesia_chc") & """,""analgesia_parafina"" : """ & i.Item("analgesia_parafina") & """,""analgesia_magneto"" : """ & i.Item("analgesia_magneto") & """,""analgesia_crio"" : """ & i.Item("analgesia_crio") & """,""analgesia_diatermia"" : """ & i.Item("analgesia_diatermia") & """,""analgesia_ondas"" : """ & i.Item("analgesia_ondas") & """,""analgesia_hidroterapia"" : """ & i.Item("analgesia_hidroterapia") & """,""analgesia_terapia_manual"" : """ & i.Item("analgesia_terapia_manual") & """,""analgesia_banios_contraste"" : """ & i.Item("analgesia_banios_contraste") & """,""analgesia_cf"" : """ & i.Item("analgesia_cf") & """,""analgesia_ejercicio"" : """ & i.Item("analgesia_ejercicio") & """,""analgesia_gimnasia"" : """ & i.Item("analgesia_gimnasia") & """,""estiramiento_sel"" : """ & i.Item("estiramiento_sel") & """,""estiramiento_activo_sup"" : """ & i.Item("estiramiento_activo_sup") & """,""estiramiento_pasivo_sup"" : """ & i.Item("estiramiento_pasivo_sup") & """,""estiramiento_cintura_escapular"" : """ & i.Item("estiramiento_cintura_escapular") & """,""estiramiento_extensores_muneca"" : """ & i.Item("estiramiento_extensores_muneca") & """,""estiramiento_extensores_codo"" : """ & i.Item("estiramiento_extensores_codo") & """,""estiramiento_flexores_muneca"" : """ & i.Item("estiramiento_flexores_muneca") & """,""estiramiento_flexores_codo"" : """ & i.Item("estiramiento_flexores_codo") & """,""estiramiento_maq_mov_pasiva_hombro"" : """ & i.Item("estiramiento_maq_mov_pasiva_hombro") & """,""estiramiento_mano_hombro"" : """ & i.Item("estiramiento_mano_hombro") & """,""estiramiento_superior_3_5"" : """ & i.Item("estiramiento_superior_3_5") & """,""estiramiento_superior_3_10"" : """ & i.Item("estiramiento_superior_3_10") & """,""estiramiento_superior_3_15"" : """ & i.Item("estiramiento_superior_3_15") & """,""estiramiento_dedos"" : """ & i.Item("estiramiento_dedos") & """,""estiramiento_otro_estiramiento"" : """ & i.Item("estiramiento_otro_estiramiento") & """,""estiramiento_activo_columna"" : """ & i.Item("estiramiento_activo_columna") & """,""estiramiento_pasivo_columna"" : """ & i.Item("estiramiento_pasivo_columna") & """,""estiramiento_paravertebrales_dorsales"" : """ & i.Item("estiramiento_paravertebrales_dorsales") & """,""estiramiento_paravertebrales_lumbares"" : """ & i.Item("estiramiento_paravertebrales_lumbares") & """,""estiramiento_cervicales"" : """ & i.Item("estiramiento_cervicales") & """,""estiramiento_paravertebrales"" : """ & i.Item("estiramiento_paravertebrales") & """,""estiramiento_columna_3_5"" : """ & i.Item("estiramiento_columna_3_5") & """,""estiramiento_columna_3_10"" : """ & i.Item("estiramiento_columna_3_10") & """,""estiramiento_columna_3_15"" : """ & i.Item("estiramiento_columna_3_15") & """,""estiramiento_trapecio"" : """ & i.Item("estiramiento_trapecio") & """,""estiramiento_activo_inf"" : """ & i.Item("estiramiento_activo_inf") & """,""estiramiento_pasivo_inf"" : """ & i.Item("estiramiento_pasivo_inf") & """,""estiramiento_cuadriceps"" : """ & i.Item("estiramiento_cuadriceps") & """,""estiramiento_tensor_fascia_lata"" : """ & i.Item("estiramiento_tensor_fascia_lata") & """,""estiramiento_isquiotibiales"" : """ & i.Item("estiramiento_isquiotibiales") & """,""estiramiento_tibial_anterior"" : """ & i.Item("estiramiento_tibial_anterior") & """,""estiramiento_aductores"" : """ & i.Item("estiramiento_aductores") & """,""estiramiento_tibial_posterior"" : """ & i.Item("estiramiento_tibial_posterior") & """,""estiramiento_abductores"" : """ & i.Item("estiramiento_abductores") & """,""estiramiento_peroneos"" : """ & i.Item("estiramiento_peroneos") & """,""estiramiento_rotadores_cadera"" : """ & i.Item("estiramiento_rotadores_cadera") & """,""estiramiento_maq_mov_pasiva_rodilla"" : """ & i.Item("estiramiento_maq_mov_pasiva_rodilla") & """,""estiramiento_fascia_plantar"" : """ & i.Item("estiramiento_fascia_plantar") & """,""estiramiento_maq_mov_pasiva_tobillo"" : """ & i.Item("estiramiento_maq_mov_pasiva_tobillo") & """,""estiramiento_inferior_3_5"" : """ & i.Item("estiramiento_inferior_3_5") & """,""estiramiento_inferior_3_10"" : """ & i.Item("estiramiento_inferior_3_10") & """,""estiramiento_inferior_3_15"" : """ & i.Item("estiramiento_inferior_3_15") & """,""fortalecimiento_sel"" : """ & i.Item("fortalecimiento_sel") & """,""fortalecimiento_cintura_escapular"" : """ & i.Item("fortalecimiento_cintura_escapular") & """,""fortalecimiento_ligas"" : """ & i.Item("fortalecimiento_ligas") & """,""fortalecimiento_polainas"" : """ & i.Item("fortalecimiento_polainas") & """,""fortalecimiento_isometricas"" : """ & i.Item("fortalecimiento_isometricas") & """,""fortalecimiento_sin_peso"" : """ & i.Item("fortalecimiento_sin_peso") & """,""fortalecimiento_extensores_muneca"" : """ & i.Item("fortalecimiento_extensores_muneca") & """,""fortalecimiento_extensores_codo"" : """ & i.Item("fortalecimiento_extensores_codo") & """,""fortalecimiento_flexores_muneca"" : """ & i.Item("fortalecimiento_flexores_muneca") & """,""fortalecimiento_flexores_codo"" : """ & i.Item("fortalecimiento_flexores_codo") & """,""fortalecimiento_maq_mov_pasiva_hombro"" : """ & i.Item("fortalecimiento_maq_mov_pasiva_hombro") & """,""fortalecimiento_mano_hombro"" : """ & i.Item("fortalecimiento_mano_hombro") & """,""fortalecimiento_superior_3_5"" : """ & i.Item("fortalecimiento_superior_3_5") & """,""fortalecimiento_superior_3_10"" : """ & i.Item("fortalecimiento_superior_3_10") & """,""fortalecimiento_superior_3_15"" : """ & i.Item("fortalecimiento_superior_3_15") & """,""fortalecimiento_dedos"" : """ & i.Item("fortalecimiento_dedos") & """,""fortalecimiento_otro_estiramiento"" : """ & i.Item("fortalecimiento_otro_estiramiento") & """,""fortalecimiento_paravertebrales_dorsales"" : """ & i.Item("fortalecimiento_paravertebrales_dorsales") & """,""fortalecimiento_williams"" : """ & i.Item("fortalecimiento_williams") & """,""fortalecimiento_mckenzic"" : """ & i.Item("fortalecimiento_mckenzic") & """,""fortalecimiento_core"" : """ & i.Item("fortalecimiento_core") & """,""fortalecimiento_klapp"" : """ & i.Item("fortalecimiento_klapp") & """,""fortalecimiento_paravertebrales_lumbares"" : """ & i.Item("fortalecimiento_paravertebrales_lumbares") & """,""fortalecimiento_cervicales"" : """ & i.Item("fortalecimiento_cervicales") & """,""fortalecimiento_paravertebrales"" : """ & i.Item("fortalecimiento_paravertebrales") & """,""fortalecimiento_columna_3_5"" : """ & i.Item("fortalecimiento_columna_3_5") & """,""fortalecimiento_columna_3_10"" : """ & i.Item("fortalecimiento_columna_3_10") & """,""fortalecimiento_columna_3_15"" : """ & i.Item("fortalecimiento_columna_3_15") & """,""fortalecimiento_trapecio"" : """ & i.Item("fortalecimiento_trapecio") & """,""fortalecimiento_ligas_columna"" : """ & i.Item("fortalecimiento_ligas_columna") & """,""fortalecimiento_isometricas_columna"" : """ & i.Item("fortalecimiento_isometricas_columna") & """,""fortalecimiento_cuadriceps"" : """ & i.Item("fortalecimiento_cuadriceps") & """,""fortalecimiento_ligas_inf"" : """ & i.Item("fortalecimiento_ligas_inf") & """,""fortalecimiento_isometricas_inf"" : """ & i.Item("fortalecimiento_isometricas_inf") & """,""fortalecimiento_tensor_fascia_lata"" : """ & i.Item("fortalecimiento_tensor_fascia_lata") & """,""fortalecimiento_isquiotibiales"" : """ & i.Item("fortalecimiento_isquiotibiales") & """,""fortalecimiento_tibial_anterior"" : """ & i.Item("fortalecimiento_tibial_anterior") & """,""fortalecimiento_aductores"" : """ & i.Item("fortalecimiento_aductores") & """,""fortalecimiento_tibial_posterior"" : """ & i.Item("fortalecimiento_tibial_posterior") & """,""fortalecimiento_abductores"" : """ & i.Item("fortalecimiento_abductores") & """,""fortalecimiento_peroneos"" : """ & i.Item("fortalecimiento_peroneos") & """,""fortalecimiento_rotadores_cadera"" : """ & i.Item("fortalecimiento_rotadores_cadera") & """,""fortalecimiento_fascia_plantar"" : """ & i.Item("fortalecimiento_fascia_plantar") & """,""fortalecimiento_polainas_inf"" : """ & i.Item("fortalecimiento_polainas_inf") & """,""fortalecimiento_sin_peso_inf"" : """ & i.Item("fortalecimiento_sin_peso_inf") & """,""fortalecimiento_banco"" : """ & i.Item("fortalecimiento_banco") & """,""fortalecimiento_trampolin"" : """ & i.Item("fortalecimiento_trampolin") & """,""fortalecimiento_bossu"" : """ & i.Item("fortalecimiento_bossu") & """,""fortalecimiento_inferior_3_5"" : """ & i.Item("fortalecimiento_inferior_3_5") & """,""fortalecimiento_inferior_3_10"" : """ & i.Item("fortalecimiento_inferior_3_10") & """,""fortalecimiento_inferior_3_15"" : """ & i.Item("fortalecimiento_inferior_3_15") & """,""reacondicionamiento_bici"" : """ & i.Item("reacondicionamiento_bici") & """,""reacondicionamiento_tiempo_bici"" : """ & i.Item("reacondicionamiento_tiempo_bici") & """,""reacondicionamiento_caminadora"" : """ & i.Item("reacondicionamiento_caminadora") & """,""reacondicionamiento_tiempo_caminadora"" : """ & i.Item("reacondicionamiento_tiempo_caminadora") & """,""reacondicionamiento_reduccion_marcha"" : """ & i.Item("reacondicionamiento_reduccion_marcha") & """,""reacondicionamiento_eliptica"" : """ & i.Item("reacondicionamiento_eliptica") & """,""reacondicionamiento_escaleras"" : """ & i.Item("reacondicionamiento_escaleras") & """,""reacondicionamiento_pelotas"" : """ & i.Item("reacondicionamiento_pelotas") & """}"
            
            End If
        Else
            Data = "{""resp"" : ""1""}"
            ' data = "[{""id"":""1"",""nombre"":""Christhian Froilan Sosa Cebalos"",""fecha_nac"":""24/04/1981"",""edad"":""37"",""origen"":""Mérida"",""ocupacion"":""Máster de la web"",""direccion"":""Vergel III"",""alergias"":""a los hombres"", ""indicaciones_medicas"":""Tomar viagra"", ""contra_indicaciones"":""No tomar viagra si tomo alcohol"",""id_diagnosticos"":""1.2.3"", ""diagnosticos"":""HOMBRO CONDROMATOSIS.HOMBRO.TOBILLO"", ""protocolo"":""3"",""fase"":""4"",""fecha"":""20/02/2019.24/02/2019.28/02/2019"",""ultrasonido"":""0"", ""chc"":""0"", ""electroterapia"":""0"", ""laser"":""1"", ""cf"":""1"", ""ejercicio"":""1"",""masaje"":""1"",""magneto"":""1"",""gimnasia"":""1"",""observaciones"":""Esta muy guapo el paciente"" }]"
            
        End If
        Return Data
    End Function
  
    Function devolverDatosdiagnosticos(ByRef idcita As String, ByRef idcliente As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
  
        
       
        
        'strSql = " select HD.IdHDiagnostico as IdHDiagnostico ,HD.IdDiagnostico as IdDiagnostico , CD.Descripcion as Descripcion , FORMAT(A.Fecha , 'dd MMMM yyyy') as FechaAgenda    " & _
        '"from agenda A  " & _
        '"inner join HMDiagnosticos HD on HD.IdCita = A.idCita " & _
        '"inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
        '"where A.idCliente  = (select idCliente from agenda where Idcita ='" + idcita + "' and HD.Status = 'True')"
        
        strSql = " select HD.IdHDiagnostico as IdHDiagnostico ,HD.IdDiagnostico as IdDiagnostico , CD.Descripcion as Descripcion , " & _
"isnull(FORMAT(HD.FechaActualizacion , 'dd MMMM yyyy'),FORMAT(A.Fecha , 'dd MMMM yyyy')) as fechaagenda " & _
"from HMDiagnosticos HD " & _
"left join agenda A  on A.IdCita = HD.idCita " & _
"inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
"where HD.idCliente  = '" + idcliente + "'  and HD.Status = 'True'"
        
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    cadena += "{""id"":""" & i.Item("IdHDiagnostico") & """, ""diagnostico"":""" & i.Item("Descripcion") & """, ""fecha"":""" & i.Item("FechaAgenda") & """}"
                Next
                cadena += "]"
                Data = cadena
            Else
                Data = "{""resp"" : ""1""}"

            End If
        Else
            
            Data = "{""resp"" : ""1""}"
            
            'Data = "[{""id"":""1"",""diagnostico"":""hombro"",""fecha"":""20-03-2018""},{""id"":""2"",""diagnostico"":""hombro2"",""fecha"":""22-02-2018""},{""id"":""3"",""diagnostico"":""hombro3"",""fecha"":""25-02-2018""}]"
        
            
        End If
        Return Data
    End Function
    
    'Function llenarprotocolos() As String
    '    Dim strSql As String
    '    Dim dt As New DataTable
    '    Dim dt2 As New DataTable
    '    Dim clsDatos As New ClaseDatos
    '    Dim Data As String = ""
  
        
       
    '    strSql = " SELECT  distinct id, nombre " & _
    '  "FROM protocolos "
        
            
        
    '    If clsDatos.cargatabla(strSql, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            Dim cadena As String
    '            Dim count As Integer = 1
    '            cadena = "["
    '            For Each i As DataRow In dt.Rows
    '                If count > 1 Then
    '                    cadena += ","
    '                End If
    '                count = count + 1
    '                cadena += "{""id"":""" & i.Item("id") & """, ""nombre"":""" & i.Item("nombre") & """}"
    '            Next
    '            cadena += "]"
    '            Data = cadena

    '        End If
    '    Else
            
    '        Data = "[{""id"":""0"",""nombre"":""Seleccionar Protocolo""}]"
        
    '        'data = "[{""id"":""0"",""nombre"":""Seleccionar Protocolo""},{""id"":""1"",""nombre"":""Protocolo 1""},{""id"":""2"",""nombre"":""Protocolo 2""},{""id"":""3"",""nombre"":""Protocolo 3""}]"
        
    '    End If
        
    '    Return Data
    'End Function
    
    Function devolverDatosprotocolos(ByRef id_protocolo As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        Dim Tratamiento As String
        Dim contraindicacion As String
        Dim observaciones As String
        Dim TratamientoM As String
        Dim contraindicacionM As String
        Dim observacionesM As String
       
       
        
        
        strSql = " select equipoejercicio,dosificacion,intensidad,tiempo,precauciones from protocolos where id='" + id_protocolo + "' "
        
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
              
                Tratamiento = "<table class='table table-striped table-hover'><thead><tr><th>Equipo/Ejercicio</th><th>Dosificación</th><th>Tiempo</th></tr></thead><tbody>"
               
                
                contraindicacion = "<table class='table table-striped table-hover'><thead><tr><th>Equipo/Ejercicio</th><th>Precauciones</th></tr></thead><tbody>"
                'contraindicacion = contraindicacion + "-".PadRight(63, "-") + Chr(10)
                
                observaciones = "<table class='table table-striped table-hover'><thead><tr><th>Equipo/Ejercicio</th><th>Intensidad</th></tr></thead><tbody>"
                'observaciones = observaciones + "-".PadRight(58, "-") + Chr(10)

                Dim count2 As Integer = 1
                For Each F As DataRow In dt.Rows
                  
                    Tratamiento = Tratamiento + "<tr><td>" + F.Item("equipoejercicio") + "</td><td>" + F.Item("dosificacion") + "</td><td>" + F.Item("tiempo") + "</td></tr>"
                    contraindicacion = contraindicacion + "<tr><td>" + F.Item("equipoejercicio") + "</td><td>" + F.Item("precauciones") + "</td></tr>"
                    observaciones = observaciones + "<tr><td>" + F.Item("equipoejercicio") + "</td><td>" + F.Item("intensidad") + "</td><td>"
       
                Next
                TratamientoM = Tratamiento + "</tbody></table>"
                contraindicacionM = contraindicacion + "</tbody></table>"
                observacionesM = observaciones + "</tbody></table>"
               
                
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                TratamientoM = Replace(TratamientoM, Chr(9), " ")
                TratamientoM = Replace(TratamientoM, Chr(10), " ")
                TratamientoM = Replace(TratamientoM, Chr(13), " ")
                TratamientoM = Replace(TratamientoM, Chr(160), " ")
                
                contraindicacionM = Replace(contraindicacionM, Chr(9), " ")
                contraindicacionM = Replace(contraindicacionM, Chr(10), " ")
                contraindicacionM = Replace(contraindicacionM, Chr(13), " ")
                contraindicacionM = Replace(contraindicacionM, Chr(160), " ")
                
                observacionesM = Replace(observacionesM, Chr(9), " ")
                observacionesM = Replace(observacionesM, Chr(10), " ")
                observacionesM = Replace(observacionesM, Chr(13), " ")
                observacionesM = Replace(observacionesM, Chr(160), " ")
                
                
                cadena += "{""tab1"":""" & TratamientoM & """,""tab2"":""" & contraindicacionM & """,""tab3"":""" & observacionesM & """}"
                'Next
                cadena += "]"
                Data = cadena

            End If
        Else
            'data = "[{""target1"":""Información del protocolo"",""target2"":""Información para la pestaña 2"",""target3"":""Pestaña 3""}]"    
        End If
        Return Data
    End Function
    
    Function devolverprotocolosguardados(ByRef idcita As String, ByRef idcliente As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
  
        
       

        
        strSql = " select HDP.idHProtocolo as IdHProtocolo ,HDP.idProtocolo as IdProtocolo , PR.nombre as Descripcion ,HDP.fase as fase, " & _
        "FORMAT(A.Fecha , 'dd MMMM yyyy') as FechaAgenda " & _
        "from agenda A  " & _
        "inner join HMProtocolos HDP on HDP.IdCita = A.idCita " & _
        "inner join protocolos PR on PR.id = HDP.idProtocolo " & _
        "where A.idCliente  = (select idCliente from agenda where Idcita ='" + idcita + "' and HDP.Status = 'True') " & _
        "group by HDP.idHProtocolo,PR.nombre,HDP.idProtocolo,HDP.fase,a.fecha"
        
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    cadena += "{""id"":""" & i.Item("IdHProtocolo") & """, ""protocolo"":""" & i.Item("Descripcion") & """,""fase"":""" & i.Item("fase") & """, ""fecha"":""" & i.Item("FechaAgenda") & """}"
                Next
                cadena += "]"
                Data = cadena

            End If
        Else
            
            'Data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""1"",""fecha"":""20-03-2018""},{""id"":""2"",""protocolo"":""PARALISIS GLUTEAL"",""fase"":""FASE 3"",""fecha"":""20-03-2018""}]"
       
        
            
        End If
        Return Data
    End Function
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class