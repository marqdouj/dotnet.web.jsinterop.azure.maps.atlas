export class Helpers {
    //Extract a specific number of elements from an array.
    static getFirstNItems<T>(arr: T[], n: number): T[] {
        if (!Array.isArray(arr)) {
            throw new Error("Input must be an array.");
        }
        if (n < 0) {
            throw new Error("Number of items must be non-negative.");
        }
        return arr.slice(0, n);
    }

    // Type-safe property access helper
    static getValue<T, K extends keyof T>(obj: T, key: K): T[K] {
        return obj[key]
    }

    /**
    * Recursively replaces all `null` values in an object with `undefined`.
    * Works for nested objects and arrays.
    */
    static nullToUndefined<T>(obj: T): T {
        if (obj === null) {
            // Replace null with undefined
            return undefined as unknown as T;
        }

        if (Array.isArray(obj)) {
            // Map through arrays
            return obj.map(item => this.nullToUndefined(item)) as unknown as T;
        }

        if (typeof obj === "object" && obj !== null) {
            // Map through object properties
            const result: any = {};
            for (const [key, value] of Object.entries(obj)) {
                result[key] = this.nullToUndefined(value);
            }
            return result;
        }

        // Return primitive values as-is
        return obj;
    }

    /**
    * Recursively removes properties with null or undefined values from an object.
    * Works with nested objects and arrays.
    */
    static removeNullish<T>(obj: T): T {
        if (obj === null || obj === undefined) {
            // If the value itself is nullish, return as-is (caller may skip it)
            return obj;
        }

        if (Array.isArray(obj)) {
            // Recursively clean each element in the array
            return obj
                .map(item => this.removeNullish(item))
                .filter(item => item !== null && item !== undefined) as unknown as T;
        }

        if (typeof obj === "object") {
            const cleaned: any = {};
            for (const [key, value] of Object.entries(obj)) {
                if (value !== null && value !== undefined) {
                    const cleanedValue = this.removeNullish(value);
                    // Only keep if not nullish after cleaning
                    if (cleanedValue !== null && cleanedValue !== undefined) {
                        cleaned[key] = cleanedValue;
                    }
                }
            }
            return cleaned;
        }

        // Primitive value (string, number, boolean, etc.)
        return obj;

        // Example usage:
        //const data = {
        //    name: "Alice",
        //    age: null,
        //    address: {
        //        street: undefined,
        //        city: "Wonderland",
        //        coords: {
        //            lat: null,
        //            lng: 123
        //        }
        //    },
        //    hobbies: [null, "reading", undefined, "coding"]
        //};

        //const cleaned = removeNullish(data);

        //console.log(cleaned);
        /*
        {
          name: "Alice",
          address: {
            city: "Wonderland",
            coords: { lng: 123 }
          },
          hobbies: ["reading", "coding"]
        }
        */
    }

    static isEmptyOrNull(str: string | null | undefined): boolean {
        return str === null || str === undefined || str.trim() === "";
    }

    static isNotEmptyOrNull(str: string | null | undefined): boolean {
        return !this.isEmptyOrNull(str);
    }

    static isValueInEnum<T extends Record<string, string>>(enumObj: T, value: string): boolean {
        return Object.values(enumObj).includes(value as T[keyof T]);
    }

    /**
    * Case-insensitive switch helper
    * @param value - The string to match
    * @param cases - An object where keys are case-insensitive match values
    * @param defaultCase - Optional default handler
    */
    static switchCaseInsensitive<T>(
        value: string,
        cases: Record<string, () => T>,
        defaultCase?: () => T
    ): T {
        const lowerValue = value.toLowerCase();
        for (const key of Object.keys(cases)) {
            if (key.toLowerCase() === lowerValue) {
                return cases[key]();
            }
        }
        if (defaultCase) return defaultCase();
        throw new Error(`No matching case for "${value}"`);
    }

    //    // Example usage
    //    const result = switchCaseInsensitive("HeLLo", {
    //        hello: () => "Matched hello",
    //        world: () => "Matched world",
    //    }, () => "Default case");

    //console.log(result); // "Matched hello"
}