import { AgendaDto } from "../Model/Agenda";

export default function ListadoAgenda(props: listadoAgendaProps)
{
    const editarAgenda = (agenda: AgendaDto) =>
    {
        Object.assign(props.agenda, agenda)

        props.abrirCerrarModalAgregarAgenda();
    }

    const eliminarAgenda = (agenda: AgendaDto) =>
    {
        Object.assign(props.agenda, agenda)

        props.abrirCerrarModalEliminarAgenda();
    }

    return(
        <>
            <div className="row">
                <table className="table table-bordered table-responsive">
                    <thead className="table-active">
                        <tr>
                            <th>Id</th>
                            <th>Nombre</th>
                            <th>Apellido P.</th>
                            <th>Apellido M.</th>
                            <th>Genero</th>
                            <th>Telefono</th>
                            <th>Celular</th>
                            <th>Email</th>
                            <th>Editar</th>
                            <th>Eliminar</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            props.listaAgenda.map(usuario =>
                                (
                                    <tr key={usuario.id}>
                                        <td>{usuario.id}</td>
                                        <td>{usuario.name}</td>
                                        <td>{usuario.middleName}</td>
                                        <td>{usuario.lastName}</td>
                                        <td>{usuario.gender}</td>
                                        <td>{usuario.telephone}</td>
                                        <td>{usuario.mobile}</td>
                                        <td>{usuario.email}</td>
                                        <td>
                                            <button className="btn btn-primary" onClick={(e) => editarAgenda(usuario)}>Editar</button>
                                        </td>
                                        <td>
                                            <button className="btn btn-danger" onClick={(e) => eliminarAgenda(usuario)}>Eliminar</button>
                                        </td>
                                    </tr>
                                ))
                        }
                    </tbody>
                </table>
            </div>
        </>
    );
}

interface listadoAgendaProps
{
    agenda: AgendaDto;
    listaAgenda: AgendaDto[];
    abrirCerrarModalAgregarAgenda(): void;
    abrirCerrarModalEliminarAgenda(): void;
}