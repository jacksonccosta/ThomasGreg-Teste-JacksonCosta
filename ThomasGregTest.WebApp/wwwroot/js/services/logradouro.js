class Logradouro {
    static url = 'logradouro';

    static getListarAxios = async (clienteId) => {
        await API.authBearer();
        return await axios.post(`${API.urlApi}${this.url}/listar`, { "clienteId": clienteId });
    }
    static postAxios = async (body) => {
        await API.authBearer();
        return await axios.post(`${API.urlApi}${this.url}`, body);
    }
    static deleteAxios = async (clienteId, id) => {
        await API.authBearer();
        return await axios.delete(`${API.urlApi}${this.url}/${clienteId}/${id}`);
    }
    static getAxios = async (clienteId, id) => {
        await API.authBearer();
        return await axios.get(`${API.urlApi}${this.url}/${clienteId}/${id}`);
    }
    static putAxios = async (body) => {
        await API.authBearer();
        return await axios.put(`${API.urlApi}${this.url}`, body);
    }
}