import React, { useState, useEffect } from 'react';
import ListadoAgenda from "../Component/ListadoAgenda";
import ModalAgregarAgenda from "../Component/ModalAgregarAgenda";
import ModalEliminarAgenda from "../Component/ModalEliminarAgenda";
import axios from 'axios'
import { AgendaDto } from '../Model/Agenda';

export default function Agenda()
{
    const baseUrlLista = process.env.React_App_baseUrlLista ? process.env.React_App_baseUrlLista : "";
    const token = process.env.React_App_token ? process.env.React_App_token : "";
    const[listaAgenda, setListaAgenda] = useState([]);
    const[agenda, setAgenda] = useState<AgendaDto>({
        id: 0,
        name: '',
        middleName: '',
        lastName: '',
        gender: '',
        telephone:'',
        mobile: '',
        email: ''
    });
    const[abrirModalAgregarAgenda, setAbrirModalAgregarAgenda] = useState(false);
    const[abrirModalEliminarAgenda, setAbrirModalEliminarAgenda] = useState(false);
    const[esGuardar, setEsGuardar] = useState(true);

    axios.interceptors.request.use((config) =>
    {
        if (!config?.headers) {
            throw new Error(`El parametro 'config.headers' no esta definido.`);
        }
        config.headers.Authorization= `Bearer ${token}`;
        return config;
    },
    error =>
    {
        console.log(error);
    });

    const listaAgendaGet = async() =>
    {
        await axios.get(baseUrlLista)
        .then(response =>
            {
                setListaAgenda(response.data);
            })
        .catch(error => {
            console.log(error);
        })
    }

    useEffect(() =>
    {
        listaAgendaGet();
    }, []);

    const abrirCerrarModalAgregarAgenda = (esGuardarAgenda: boolean) => {
        setAbrirModalAgregarAgenda(!abrirModalAgregarAgenda);
        setEsGuardar(esGuardarAgenda);
        if(esGuardarAgenda)
        {
            var agendaNuevo = agenda;
            agendaNuevo.id = 0;
            agendaNuevo.name = '';
            agendaNuevo.middleName = '';
            agendaNuevo.lastName = '';
            agendaNuevo.gender = '';
            agendaNuevo.telephone = '';
            agendaNuevo.mobile = '';
            agendaNuevo.email = '';

            setAgenda(agendaNuevo);
        }
    }

    const abrirCerrarModalEliminarAgenda = () => {
        setAbrirModalEliminarAgenda(!abrirModalEliminarAgenda);
    }

    return(
        <>
            <div className="container">
                <hr />
                <h3>Agenda Electronica</h3>
                <hr />
                <button className="btn btn-success" onClick={() => abrirCerrarModalAgregarAgenda(true)}>Agregar</button>
                <br />
                <br />
                <ListadoAgenda listaAgenda={listaAgenda} agenda={agenda} abrirCerrarModalAgregarAgenda={() => abrirCerrarModalAgregarAgenda(false)}
                    abrirCerrarModalEliminarAgenda={() => abrirCerrarModalEliminarAgenda()}/>
                <ModalAgregarAgenda agenda={agenda} listaAgenda={listaAgenda} esGuardar={esGuardar} abrirModalAgregarAgenda={abrirModalAgregarAgenda}
                    abrirCerrarModalAgregarAgenda={() => abrirCerrarModalAgregarAgenda(esGuardar)} />
                <ModalEliminarAgenda agenda={agenda} listaAgenda={listaAgenda} abrirModalEliminarAgenda={abrirModalEliminarAgenda}
                    abrirCerrarModalEliminarAgenda={() => abrirCerrarModalEliminarAgenda()}/>
            </div>
        </>
    );
}
