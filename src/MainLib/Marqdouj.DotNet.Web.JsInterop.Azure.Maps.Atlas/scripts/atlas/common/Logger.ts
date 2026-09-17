export enum LogLevel {
    Trace = 0,
    Debug = 1,
    Information = 2,
    Warn = 3,
    Error = 4,
    Critical = 5,
    None = 6
}

export class Logger {
    static currentLevel: LogLevel = LogLevel.Information;

    static #GetMapHeader(mapId: string): string {
        return `Map with Id '${mapId}'`;
    }

    static logMapMessageAndThrow(mapId: string, message: string, ...optionalParams: any[]) {
        this.logMapMessage(mapId, LogLevel.Error, message, optionalParams);
        throw new Error(message);
    }

    static logMapMessage(mapId: string, level: LogLevel, message: string, ...optionalParams: any[]): void {
        if (level < this.currentLevel)
            return;

        const logOutput = `${this.#GetMapHeader(mapId)} [${Logger.#logLevelName(level)}] ${message}`;
        this.#logToConsole(logOutput, level, ...optionalParams);
    }

    static logMessage(header: string, level: LogLevel, message: string, ...optionalParams: any[]): void {
        if (level < this.currentLevel)
            return;

        const logOutput = `${header} [${Logger.#logLevelName(level)}] ${message}`;
        this.#logToConsole(logOutput, level, ...optionalParams);
    }

    static #logToConsole(logOutput: string, level: LogLevel, ...optionalParams: any[]) {
        switch (level) {
            case LogLevel.Trace:
                console.trace(logOutput, ...optionalParams);
                break;
            case LogLevel.Debug:
                console.debug(logOutput, ...optionalParams);
                break;
            case LogLevel.Information:
                console.info(logOutput, ...optionalParams);
                break;
            case LogLevel.Warn:
                console.warn(logOutput, ...optionalParams);
                break;
            case LogLevel.Error:
                console.error(logOutput, ...optionalParams);
                break;
            case LogLevel.Critical:
                console.error(`CRITICAL: ${logOutput}`, ...optionalParams);
                break;
        }
    }

    static #logLevelName(level: LogLevel): string {
        return LogLevel[level];
    }
}
