const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? "http://localhost:5102";

export async function apiRequest(path, options = {}) {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      "Content-Type": "application/json",
      ...(options.headers ?? {})
    },
    ...options
  });

  const text = await response.text();
  const payload = text ? JSON.parse(text) : null;

  if (!response.ok) {
    const message = payload?.mensaje ?? payload?.motivo ?? `Error HTTP ${response.status}`;
    throw new Error(message);
  }

  return payload;
}

export { API_BASE_URL };
