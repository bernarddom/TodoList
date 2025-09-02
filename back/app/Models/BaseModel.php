<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class BaseModel extends Model
{
    // Common reusable logic for all models
    protected $guarded = []; // allow mass assignment unless specified
    
    public static function getAll($columns = ['*'])
    {
        return static::all($columns);
    }

    public function saveModel(array $data)
    {
        $this->fill($data);
        $this->save();
        return $this;
    }
    public static function findByKey($key)
    {
        return static::where('id', $key)
            ->orWhere('uuid', $key)
            ->first();
    }
}