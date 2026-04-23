import React, { useEffect, useMemo, useState } from "react";
import { createRoot } from "react-dom/client";
import { API_BASE_URL, apiRequest } from "./api";
import { modules, sampleBodies } from "./modules";
import "./styles.css";

const airportStats = [
  { label: "Vuelos activos", value: "42", hint: "salidas y llegadas" },
  { label: "Hangares", value: "12", hint: "en operacion" },
  { label: "Puertas", value: "18", hint: "terminal central" },
  { label: "Equipaje", value: "96%", hint: "flujo normal" }
];

const flightBoard = [
  { time: "06:40", flight: "AUR 218", city: "Panama", gate: "B4", status: "Abordando" },
  { time: "07:15", flight: "GUA 502", city: "Miami", gate: "C1", status: "A tiempo" },
  { time: "08:05", flight: "MAY 113", city: "San Jose", gate: "A7", status: "Revision" },
  { time: "09:20", flight: "AUR 781", city: "Madrid", gate: "D2", status: "Programado" }
];

function formatValue(value) {
  if (value === null || value === undefined) return "-";
  if (typeof value === "object") return JSON.stringify(value);
  return String(value);
}

function getPrimaryKey(row) {
  if (!row || typeof row !== "object") return "";
  const key = getPrimaryKeyName(row);
  return key ? row[key] : "";
}

function getPrimaryKeyName(row) {
  if (!row || typeof row !== "object") return "";
  if (Object.hasOwn(row, "id")) return "id";
  if (Object.hasOwn(row, "Id")) return "Id";
  return Object.keys(row)[0] ?? "";
}

function buildFormData(source = {}) {
  const primaryKey = getPrimaryKeyName(source);
  return Object.fromEntries(Object.entries(source).filter(([key]) => key !== primaryKey));
}

function normalizeValue(value) {
  if (value === "") return null;
  if (value === "true") return true;
  if (value === "false") return false;
  if (value !== null && value !== undefined && value !== "" && !Number.isNaN(Number(value)) && /^-?\d+(\.\d+)?$/.test(String(value))) {
    return Number(value);
  }
  return value;
}

function formatInputValue(value) {
  if (value === null || value === undefined) return "";
  if (typeof value === "string" && value.includes("T")) return value.slice(0, 16);
  return String(value);
}

function getInputType(key, value) {
  const lower = key.toLowerCase();
  if (typeof value === "number" || lower.includes("costo") || lower.includes("precio") || lower.includes("cantidad") || lower.startsWith("id")) {
    return "number";
  }
  if (lower.includes("fecha") && typeof value === "string" && !value.includes("T")) return "date";
  if (lower.includes("fecha") || lower.includes("hora")) return "datetime-local";
  return "text";
}

function App() {
  const [selected, setSelected] = useState(modules[0]);
  const [rows, setRows] = useState([]);
  const [status, setStatus] = useState("Listo para conectar con el API.");
  const [loading, setLoading] = useState(false);
  const [recordId, setRecordId] = useState("");
  const [selectedRecord, setSelectedRecord] = useState(null);
  const [formData, setFormData] = useState(sampleBodies[modules[0].route] ?? {});
  const [searchTerm, setSearchTerm] = useState("");
  const [moduleTerm, setModuleTerm] = useState("");

  const columns = useMemo(() => {
    const first = rows[0];
    return first ? Object.keys(first).slice(0, 8) : [];
  }, [rows]);

  const visibleRows = useMemo(() => {
    const term = searchTerm.trim().toLowerCase();
    if (!term) return rows;
    return rows.filter((row) => JSON.stringify(row).toLowerCase().includes(term));
  }, [rows, searchTerm]);

  const visibleModules = useMemo(() => {
    const term = moduleTerm.trim().toLowerCase();
    if (!term) return modules;
    return modules.filter((module) => `${module.name} ${module.route}`.toLowerCase().includes(term));
  }, [moduleTerm]);

  const formFields = useMemo(() => Object.entries(formData), [formData]);

  useEffect(() => {
    setFormData(sampleBodies[selected.route] ?? {});
    setSelectedRecord(null);
    setRecordId("");
    setSearchTerm("");
    loadRows(selected);
  }, [selected]);

  async function loadRows(module = selected) {
    setLoading(true);
    setStatus(`Cargando ${module.name}...`);

    try {
      const data = await apiRequest(`/api/${module.route}`);
      const nextRows = Array.isArray(data) ? data : [data];
      setRows(nextRows);
      if (!sampleBodies[module.route] && nextRows[0]) {
        setFormData(buildFormData(nextRows[0]));
      }
      setStatus(`${module.name}: informacion sincronizada con operaciones.`);
    } catch (error) {
      setRows([]);
      setStatus(error.message);
    } finally {
      setLoading(false);
    }
  }

  async function checkConnection() {
    setLoading(true);
    try {
      const data = await apiRequest("/api/Test/conexion");
      setStatus(typeof data === "string" ? data : JSON.stringify(data));
    } catch (error) {
      setStatus(error.message);
    } finally {
      setLoading(false);
    }
  }

  async function loadById() {
    if (!recordId) {
      setStatus("Selecciona una fila o escribe un ID para consultar.");
      return;
    }

    setLoading(true);
    try {
      const data = await apiRequest(`/api/${selected.route}/${recordId}`);
      selectRecord(data);
      setStatus(`${selected.name}: registro ${recordId} cargado.`);
    } catch (error) {
      setStatus(error.message);
    } finally {
      setLoading(false);
    }
  }

  function selectRecord(row) {
    setSelectedRecord(row);
    setRecordId(String(getPrimaryKey(row)));
    setFormData(buildFormData(row));
  }

  function startNewRecord() {
    setSelectedRecord(null);
    setRecordId("");
    setFormData(sampleBodies[selected.route] ?? buildFormData(rows[0] ?? {}));
    setStatus(`${selected.name}: formulario listo para nuevo registro.`);
  }

  function updateField(key, value) {
    setFormData((current) => ({
      ...current,
      [key]: value
    }));
  }

  async function sendMutation(method) {
    try {
      const needsId = method !== "POST";
      if (needsId && !recordId) {
        setStatus("Selecciona un registro o escribe un ID para actualizar o eliminar.");
        return;
      }

      const options = { method };
      if (method !== "DELETE") {
        const payload = Object.fromEntries(Object.entries(formData).map(([key, value]) => [key, normalizeValue(value)]));
        options.body = JSON.stringify(payload);
      }

      setLoading(true);
      const suffix = needsId ? `/${recordId}` : "";
      const data = await apiRequest(`/api/${selected.route}${suffix}`, options);
      setStatus(data?.mensaje ?? `${selected.name}: operacion completada.`);
      await loadRows();
      if (method === "POST") startNewRecord();
    } catch (error) {
      setStatus(error.message);
    } finally {
      setLoading(false);
    }
  }

  return (
    <main className="shell">
      <section className="hero">
        <div className="heroCopy">
          <p className="eyebrow">Aeropuerto Internacional La Aurora</p>
          <h1>Centro de control aeroportuario.</h1>
          <p className="heroText">
            Monitorea vuelos, hangares, pasajeros, empleados y operaciones conectadas al backend en{" "}
            <strong>{API_BASE_URL}</strong>.
          </p>
          <div className="heroActions">
            <button onClick={() => loadRows()} disabled={loading}>
              Sincronizar datos
            </button>
            <button className="ghost" onClick={checkConnection} disabled={loading}>
              Verificar Oracle
            </button>
          </div>
        </div>

        <div className="towerCard">
          <div className="towerTop">
            <span className={loading ? "pulse" : "dot"} />
            <p>Torre de control</p>
          </div>
          <div className="radar">
            <span />
          </div>
          <small>{status}</small>
        </div>
      </section>

      <section className="airportOverview">
        {airportStats.map((stat) => (
          <article className="statCard" key={stat.label}>
            <span>{stat.label}</span>
            <strong>{stat.value}</strong>
            <small>{stat.hint}</small>
          </article>
        ))}

        <article className="flightBoard">
          <div className="boardHeader">
            <span>Salidas proximas</span>
            <strong>Terminal A</strong>
          </div>
          {flightBoard.map((flight) => (
            <div className="flightRow" key={flight.flight}>
              <span>{flight.time}</span>
              <strong>{flight.flight}</strong>
              <p>{flight.city}</p>
              <em>{flight.gate}</em>
              <small>{flight.status}</small>
            </div>
          ))}
        </article>
      </section>

      <section className="layout">
        <aside className="sidebar">
          <div className="terminalSign">
            <strong>Modulos</strong>
            <span>Operacion aeroportuaria</span>
          </div>
          <input
            className="moduleSearch"
            value={moduleTerm}
            onChange={(event) => setModuleTerm(event.target.value)}
            placeholder="Buscar modulo..."
          />

          <div className="moduleList">
            {visibleModules.map((module) => (
              <button
                key={module.route}
                className={module.route === selected.route ? "module active" : "module"}
                onClick={() => setSelected(module)}
              >
                <span>{module.icon}</span>
                {module.name}
              </button>
            ))}
          </div>
        </aside>

        <section className="workspace">
          <div className="toolbar">
            <div>
              <p className="eyebrow">Mesa operativa</p>
              <h2>{selected.name}</h2>
            </div>
            <div className="toolbarActions">
              <button className="ghostButton" onClick={startNewRecord} disabled={loading}>
                Nuevo
              </button>
              <button onClick={() => loadRows()} disabled={loading}>
                Refrescar
              </button>
            </div>
          </div>

          <div className="grid">
            <article className="panel tablePanel">
              <div className="panelHeader">
                <h3>Registros</h3>
                <span>{visibleRows.length} de {rows.length} filas</span>
              </div>
              <input
                className="recordSearch"
                value={searchTerm}
                onChange={(event) => setSearchTerm(event.target.value)}
                placeholder={`Buscar en ${selected.name.toLowerCase()}...`}
              />

              {visibleRows.length === 0 ? (
                <div className="empty">
                  <strong>No hay datos visibles.</strong>
                  <p>Puede ser que la tabla este vacia o que el API haya devuelto un error.</p>
                </div>
              ) : (
                <div className="tableWrap">
                  <table>
                    <thead>
                      <tr>
                        {columns.map((column) => (
                          <th key={column}>{column}</th>
                        ))}
                        <th>ID</th>
                      </tr>
                    </thead>
                    <tbody>
                      {visibleRows.map((row, index) => (
                        <tr
                          key={`${getPrimaryKey(row)}-${index}`}
                          className={String(getPrimaryKey(row)) === String(recordId) ? "selectedRow" : ""}
                          onClick={() => selectRecord(row)}
                        >
                          {columns.map((column) => (
                            <td key={column}>{formatValue(row[column])}</td>
                          ))}
                          <td>{getPrimaryKey(row)}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              )}
            </article>

            <article className="panel formPanel">
              <div className="panelHeader">
                <h3>{selectedRecord ? "Editar registro" : "Nuevo registro"}</h3>
                <span>/api/{selected.route}</span>
              </div>

              <div className="lookupRow">
                <label>
                  ID operativo
                  <input value={recordId} onChange={(event) => setRecordId(event.target.value)} placeholder="Ej. 1" />
                </label>
                <button className="ghostButton" onClick={loadById} disabled={loading}>
                  Cargar
                </button>
              </div>

              <div className="formGrid">
                {formFields.length === 0 ? (
                  <div className="empty compact">
                    <strong>Sin plantilla de campos.</strong>
                    <p>Refresca el modulo o selecciona una fila para editar.</p>
                  </div>
                ) : (
                  formFields.map(([key, value]) => (
                    <label key={key}>
                      {key}
                      <input
                        type={getInputType(key, value)}
                        value={formatInputValue(value)}
                        onChange={(event) => updateField(key, event.target.value)}
                      />
                    </label>
                  ))
                )}
              </div>

              <div className="actions">
                <button onClick={() => sendMutation("POST")} disabled={loading}>
                  Crear
                </button>
                <button onClick={() => sendMutation("PUT")} disabled={loading}>
                  Actualizar
                </button>
                <button className="danger" onClick={() => sendMutation("DELETE")} disabled={loading}>
                  Eliminar
                </button>
              </div>
            </article>
          </div>
        </section>
      </section>
    </main>
  );
}

createRoot(document.getElementById("root")).render(<App />);
